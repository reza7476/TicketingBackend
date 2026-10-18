using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Domain.Entities.TicketReplies;
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
    private readonly DbSet<TicketReply> _replies;

    public TicketAppRepository(AppDbContext context)
    {
        _tickets = context.Set<Ticket>();
        _users = context.Set<User>();
        _replies = context.Set<TicketReply>();
    }

    public async Task Add(Ticket ticket)
    {
        await _tickets.AddAsync(ticket);
    }

    public async Task<Ticket?> FindById(string id)
    {
        return await _tickets.FindAsync(id);
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
        if (filter != null)
        {
            if (filter.priority != null)
            {

                query = query.Where(_ => _.Priority == filter.priority);
            }
        }

        query = query.OrderByDescending(t => t.CreatedAt);
        return await query.Paginate(pagination ?? new Pagination());
    }

    public async Task<IPageResult<GetAllTicketsDto>> GetAllForClient(
        string userId,
        TicketFilterDto? filter,
        IPagination? pagination)
    {
        var query = (
             from ticket in _tickets
             join user in _users
             on ticket.UserId equals user.Id
             where ticket.UserId == userId
             select new GetAllTicketsDto()
             {
                 AskedBy = user.Mobile,
                 CreatedAt = ticket.CreatedAt,
                 Id = ticket.Id,
                 Status = ticket.Status,
                 Title = ticket.Title,
                 Priority = ticket.Priority
             }).AsQueryable();

        if (filter != null)
        {
            if (filter.priority != null)
            {

                query = query.Where(_ => _.Priority == filter.priority);
            }
        }

        query = query.OrderByDescending(t => t.CreatedAt);
        return await query.Paginate(pagination ?? new Pagination());

    }

    public Task<GetTicketDetailsDto?> GetDetailsForAgent(string id)
    {
        var users = _users;

        var query = (
             from ticket in _tickets
             join owner in users
             on ticket.UserId equals owner.Id
             where ticket.Id == id
             join reply in _replies
             on ticket.Id equals reply.TicketId into ticketReplies

             select new GetTicketDetailsDto()
             {
                 AskedBy = owner.Mobile,
                 Content = ticket.Content,
                 CreatedAt = ticket.CreatedAt,
                 Priority = ticket.Priority,
                 Status = ticket.Status,
                 Title = ticket.Title,
                 Replies = (
                    from reply in ticketReplies
                    join replyUser in users
                        on reply.UserId equals replyUser.Id
                    select new TicketRepliedDto
                    {
                        RepliedAt = reply.CreatedAt,
                        RepliedBy = replyUser.Mobile,
                        Content = reply.Content,
                        AgentReplied = reply.UserId != ticket.UserId
                    }).ToList()
             }).FirstOrDefaultAsync();
        return query;
    }
}
