using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Application.Services.Entities.Users.Exceptions;
using Ticketing.Domain.Entities.Tickets;
using Ticketing.Shared.Interfaces;

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
        if(!await _userRepository.ExistById(userId))
        {
            throw new UserNotFoundException();
        }

        var ticket = new Ticket()
        {
            Id=Guid.NewGuid().ToString(),
            CreatedAt=_dateTimeService.Now,
            Content=dto.Content,
            Priority=dto.Priority,
            Title=dto.Title,
            Status=TicketStatus.Open,
            UserId = userId!
        };

        await _repository.Add(ticket);
        await _unitOfWork.Complete();
        return ticket.Id;

    }
}
