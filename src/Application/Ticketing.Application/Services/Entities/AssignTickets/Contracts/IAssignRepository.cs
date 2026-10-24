using System.Collections.Generic;
using Ticketing.Domain.Entities.TicketAssignments;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.AssignTickets.Contracts;

public interface IAssignRepository : IRepository
{
    Task Add(TicketAssignment assign);
}
