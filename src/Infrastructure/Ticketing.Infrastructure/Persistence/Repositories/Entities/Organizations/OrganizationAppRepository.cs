using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Organizations.Contracts;
using Ticketing.Domain.Entities.Organizations;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.Organizations;
public class OrganizationAppRepository : IOrganizationRepository
{
    private readonly DbSet<Organization> _organizations;


    public OrganizationAppRepository(AppDbContext context)
    {
        _organizations = context.Set<Organization>();
    }

    public async Task Add(Organization organ)
    {
        await _organizations.AddAsync(organ);   
    }

    public async Task<bool> ExistedByName(string name)
    {
        return await _organizations.AnyAsync(o=>o.Name == name);
    }
}
