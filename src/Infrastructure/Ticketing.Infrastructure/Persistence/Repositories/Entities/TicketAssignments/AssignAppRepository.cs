using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.AssignTickets.Contracts;
using Ticketing.Domain.Entities.TicketAssignments;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.TicketAssignments;

public class AssignAppRepository:IAssignRepository
{
    private readonly DbSet<TicketAssignment> _assignments;

    
    public AssignAppRepository(AppDbContext context)
    {
            _assignments=context.Set<TicketAssignment>();
    }

    public async Task Add(TicketAssignment assign)
    {
        await _assignments.AddAsync(assign);
    }
}
