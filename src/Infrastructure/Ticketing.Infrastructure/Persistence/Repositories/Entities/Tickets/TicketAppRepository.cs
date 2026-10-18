using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Domain.Entities.Tickets;
using Ticketing.Domain.Entities.Users;
using Ticketing.Infrastructure.Persistence.DatabaseContext;
using Ticketing.Infrastructure.Persistence.Extensions.Paginations;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.Tickets;

public class TicketAppRepository : ITicketRepository
{

    private readonly DbSet<Ticket> _tickets;
    private readonly DbSet<User> _users;


    public TicketAppRepository(AppDbContext context)
    {
        _tickets = context.Set<Ticket>();
        _users = context.Set<User>();
    }

    public async Task Add(Ticket ticket)
    {
        await _tickets.AddAsync(ticket);
    }

    public async Task<IPageResult<GetAllTicketsDto>>
        GetAllForAgent(IPagination? pagination, TicketFilterDto? filter)
    {
        var query = (

            from ticket in _tickets
            join user in _users
            on ticket.UserId equals user.Id
            select new GetAllTicketsDto()
            {
                AskedBy = user.Mobile,
                CreatedAt = ticket.CreatedAt,
                Id = ticket.Id,
                Status = ticket.Status,
                Title = ticket.Title,
                Priority = ticket.Priority
            }).AsQueryable();
        if(filter != null) 
        {
            query = query.Where(_ => _.Priority == filter.priority);
        }

        query = query.OrderByDescending(t => t.CreatedAt);
        return await query.Paginate(pagination ?? new Pagination());
    }
}
