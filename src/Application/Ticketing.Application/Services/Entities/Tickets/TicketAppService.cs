using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Application.Services.Entities.Tickets.Exceptions;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Application.Services.Entities.Users.Exceptions;
using Ticketing.Domain.Entities.Tickets;
using Ticketing.Shared.Interfaces;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Application.Services.Entities.Tickets;

public class TicketAppService : ITicketService
{

    private readonly ITicketRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;
    private readonly IUserRepository _userRepository;


    public TicketAppService(
        ITicketRepository repository,
        IUnitOfWork unitOfWork,
        IDateTimeService dateTimeService,
        IUserRepository userRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
        _userRepository = userRepository;
    }

    public async Task<string> Add(AddTicketDto dto, string? userId)
    {
        if (!await _userRepository.ExistById(userId))
        {
            throw new UserNotFoundException();
        }

        var ticket = new Ticket()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId!,
            Title = dto.Title,
            Content = dto.Content,
            Priority = dto.Priority,
            Status = TicketStatus.Open,
            CreatedAt = _dateTimeService.Now,
        };

        await _repository.Add(ticket);
        await _unitOfWork.Complete();
        return ticket.Id;

    }

    public async Task ChangeStatusByAgent(ChangeTicketStatusDto dto)
    {
        var ticket = await _repository.FindById(dto.TicketId);
        if (ticket == null)
        {
            throw new TicketNotFoundException();
        }

        if (ticket.Status == TicketStatus.Closed)
        {
            throw new TicketStatusWasChangedException();
        }
        ticket.Status = dto.Status;
        await _unitOfWork.Complete();
    }

    public async Task ChangeStatusByClient(ChangeTicketStatusByClientDto dto)
    {
        var ticket = await _repository.FindById(dto.TicketId);
        if (ticket == null)
        {
            throw new TicketNotFoundException();
        }
        if (ticket.UserId != dto.UserId)
        {
            throw new ClientDoesNotAllowChangeStatusException();
        }

        if (ticket.Status != TicketStatus.Answered)
        {
            throw new ClientDoesNotAllowChangeStatusException();
        }

        ticket.Status = TicketStatus.InProgress;
        await _unitOfWork.Complete();
    }

    public async Task ChangeStatusIntoAnswered(string ticketId)
    {
        var ticket = await _repository.FindById(ticketId);

        if (ticket == null)
        {
            throw new TicketNotFoundException();
        }

        if (ticket.Status == TicketStatus.Closed)
        {
            throw new TicketStatusWasChangedException();
        }

        ticket.Status = TicketStatus.Answered;
        await _unitOfWork.Complete();
    }

    public async Task<IPageResult<GetAllTicketsDto>>
        GetAllForAgent(IPagination? pagination = null,
        TicketFilterDto? filter = null)
    {
        return await _repository.GetAllForAgent(pagination, filter);
    }

    public async Task<IPageResult<GetAllTicketsDto>> GetAllForClient(
        string userId,
        TicketFilterDto? filter, 
        IPagination? pagination)
    {


        return await _repository.GetAllForClient(userId,filter, pagination);
    }

    public async Task<GetTicketDetailsDto?> GetDetailsForAgent(string id)
    {
        return await _repository.GetDetailsForAgent(id);
    }
}
