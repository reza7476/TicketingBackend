using Microsoft.EntityFrameworkCore.Storage;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Infrastructure.Persistence.DatabaseContext;

public class AppUnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;



    public AppUnitOfWork(AppDbContext context)
    {
        _context = context;
    }


    public async Task Begin()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
        if (_transaction != null)
            await _transaction.CommitAsync();
    }

    public  async Task Complete()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RoleBack()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }
}
