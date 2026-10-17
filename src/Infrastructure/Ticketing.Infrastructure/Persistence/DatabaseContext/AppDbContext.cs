using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ticketing.Infrastructure.Persistence.DatabaseContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(
        Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }


}
