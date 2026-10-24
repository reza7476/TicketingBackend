using Ticketing.Application.Services.Entities.AssignTickets.Contracts;
using Ticketing.Application.Services.Entities.AssignTickets.Contracts.Dto;
using Ticketing.Domain.Entities.TicketAssignments;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.Services.Entities.AssignTickets;

public class AssignAppService : IAssignService
{
    private readonly IAssignRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;

    public AssignAppService(
        IAssignRepository repository,
        IUnitOfWork unitOfWork,
        IDateTimeService dateTimeService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task Add(AddAssignTicketDto dto)
    {

        var assign = new TicketAssignment()
        {
            AssignedToUserId = dto.AgentId,
            CreatedAt = _dateTimeService.Now,
            Id = Guid.NewGuid().ToString(),
            TicketId = dto.TicketId,
        };

        await _repository.Add(assign);
        await _unitOfWork.Complete();
    }
}
