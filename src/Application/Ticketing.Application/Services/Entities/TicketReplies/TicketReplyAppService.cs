using Ticketing.Application.Services.Entities.TicketReplies.Contracts;
using Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;
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

    public async Task<string> AddReply(AddTicketReplyDto dto)
    {
        var reply = new TicketReply()
        {
            Content = dto.Content,
            CreatedAt = _dateTimeService.Now,
            Id = Guid.NewGuid().ToString(),
            TicketId = dto.TicketId,
            UserId = dto.UserId,

        };
        await _repository.Add(reply);
        await _unitOfWork.Complete();
        return reply.Id;
    }
}
