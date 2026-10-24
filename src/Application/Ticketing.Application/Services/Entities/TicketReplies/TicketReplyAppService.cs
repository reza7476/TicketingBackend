using Ticketing.Application.Services.Entities.TicketReplies.Contracts;
using Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;
using Ticketing.Application.Services.Entities.TicketReplies.Exceptions;
using Ticketing.Application.Services.Entities.Tickets.Exceptions;
using Ticketing.Application.UseCases.TicketReplies.Contracts.Dto;
using Ticketing.Domain.Entities.TicketReplies;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.Services.Entities.TicketReplies;

public class TicketReplyAppService : ITicketReplyService
{

    private readonly IDateTimeService _dateTimeService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITicketReplyRepository _repository;

    public TicketReplyAppService(
        IDateTimeService dateTimeService,
        IUnitOfWork unitOfWork,
        ITicketReplyRepository repository)
    {
        _dateTimeService = dateTimeService;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<string> Add(
        AddTicketReplyDto dto, 
        string userId,
        string userRole)
    {
        if(!await _repository.TicketIsExistById(dto.TicketId))
        {
            throw new TicketNotFoundException();
        }

        var reply = new TicketReply()
        {
            Id = Guid.NewGuid().ToString(),
            ApprovedByAdmin = false,
            Content = dto.Content,
            CreatedAt = _dateTimeService.Now,
            IssuedBy = userRole,
            TicketId = dto.TicketId,
            UserId = userId,
        };

        await _repository.Add(reply);
        await _unitOfWork.Complete();
        return reply.Id;
    }

    public async Task<string> Approve(string id)
    {

        var reply = await _repository.FindById(id);
        if (reply == null)
        {
            throw new TicketReplyNotFoundException();
        }

        reply.ApprovedByAdmin = true;
        await _unitOfWork.Complete();
        return reply.TicketId;
    }

    public async Task Edit(string id, EditTicketReplyDto dto)
    {
        var reply = await _repository.FindById(id);
        if(reply == null)
        {
            throw new TicketReplyNotFoundException();
        }


        reply.Content = dto.Content;
        await _unitOfWork.Complete();   
    }
}
