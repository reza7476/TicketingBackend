using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.UseCases.Tickets.Contracts;

public interface ITicketHandler : IService
{
    Task RemoveDocument(string documentId, string id, string userId);
}
