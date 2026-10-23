using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Domain.Entities.TicketAssignments;
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
    private readonly DbSet<TicketAssignment> _assignments;


    public TicketAppRepository(AppDbContext context)
    {
        _tickets = context.Set<Ticket>();
        _users = context.Set<User>();
        _replies = context.Set<TicketReply>();
        _assignments = context.Set<TicketAssignment>();
    }

    public async Task Add(Ticket ticket)
    {
        await _tickets.AddAsync(ticket);
    }

    public async Task<Ticket?> FindById(string id)
    {
        return await _tickets.FindAsync(id);
    }

    public async Task<List<Ticket>> GetAllAnsweredTickets()
    {
        var twoHoursAgo = DateTime.UtcNow.AddMinutes(-2);

        var query =
            from ticket in _tickets
            join reply in _replies
                on ticket.Id equals reply.TicketId
            where ticket.Status == TicketStatus.Answered
            group reply by ticket into repliesGroup
            where repliesGroup.Max(r => r.CreatedAt) <= twoHoursAgo
            select repliesGroup.Key;

        return await query.ToListAsync();
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
                Priority = ticket.Priority,
                Number = ticket.Number,
            }).AsQueryable();
        if (filter != null)
        {
            if (filter.priority != null)
            {

                query = query.Where(_ => _.Priority == filter.priority);
            }
        }

        query = query.OrderBy(t => t.Number);
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
                 Priority = ticket.Priority,
                 Number=ticket.Number,
             }).AsQueryable();

        if (filter != null)
        {
            if (filter.priority != null)
            {
                query = query.Where(_ => _.Priority == filter.priority);
            }
        }

        query = query.OrderBy(t => t.Number);
        return await query.Paginate(pagination ?? new Pagination());

    }

    public async Task<GetTicketDetailsDto?> GetDetailsForAdmin(string id)
    {
        var query = await (
            from ticket in _tickets
            join owner in _users
                on ticket.UserId equals owner.Id
            where ticket.Id == id
            select new GetTicketDetailsDto
            {
                AskedBy = owner.Mobile,
                OwnerOrgan = owner.Organization.Name,
                Content = ticket.Content,
                CreatedAt = ticket.CreatedAt,
                Priority = ticket.Priority,
                Status = ticket.Status,
                Title = ticket.Title,
                Number = ticket.Number,

                Replies = (
                    from reply in _replies
                    join replyUser in _users
                        on reply.UserId equals replyUser.Id
                    where reply.TicketId == ticket.Id
                    orderby reply.CreatedAt
                    select new TicketRepliedDto
                    {
                        Id = reply.Id,
                        RepliedAt = reply.CreatedAt,
                        RepliedBy = replyUser.FirstName + " " + replyUser.LastName,
                        Content = reply.Content,
                        ApprovedByAdmin = reply.ApprovedByAdmin,
                        IssuedBy = reply.IssuedBy
                    }
                ).ToList(),

                AssignmentHistory = (
                    from assignment in _assignments
                    join assignedUser in _users
                        on assignment.AssignedToUserId equals assignedUser.Id
                    where assignment.TicketId == ticket.Id
                    orderby assignment.CreatedAt
                    select new AssignmentHistoryDto
                    {
                        AssignedUser = assignedUser.FirstName + " " + assignedUser.LastName,
                        CreatedAt = assignment.CreatedAt
                    }
                ).ToList()
            }
        ).FirstOrDefaultAsync();

        return query;
    }

    public Task<GetTicketDetailsDto?> GetDetailsForClient(string id, string userId)
    {
        var users = _users;

        var query = (
             from ticket in _tickets
             join owner in users
             on ticket.UserId equals owner.Id
             where ticket.Id == id && ticket.UserId == userId
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
                 Number = ticket.Number,
                 Replies = (
                    from reply in ticketReplies
                    join replyUser in users
                        on reply.UserId equals replyUser.Id
                    select new TicketRepliedDto
                    {
                        RepliedAt = reply.CreatedAt,
                        RepliedBy = replyUser.Mobile,
                        Content = reply.Content,
                    }).ToList()
             }).FirstOrDefaultAsync();
        return query;
    }
}
