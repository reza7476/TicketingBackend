using Ticketing.Application.Services.Entities.AssignTickets.Contracts;
using Ticketing.Application.Services.Entities.Documents.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Application.UseCases.Tickets.Contracts;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.UseCases.Tickets;

public class TicketHandler : ITicketHandler
{

    private readonly ITicketService _ticketService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentService _documentService;

    public TicketHandler(
        ITicketService ticketService,
        IUnitOfWork unitOfWork, 
        IDocumentService documentService)
    {
        _ticketService = ticketService;
        _unitOfWork = unitOfWork;
        _documentService = documentService;
    }

    public async Task RemoveDocument(string documentId, string id, string userId)
    {
        await _unitOfWork.Begin();

        try
        {
            await _ticketService.RemoveDocument(new RemoveDocumentDto()
            {
                DocumentId = documentId,
                TicketId = id,
                UserId = userId
            });

            await _documentService.Remove(documentId);

            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.RoleBack();
            throw;
        }
    }
}
