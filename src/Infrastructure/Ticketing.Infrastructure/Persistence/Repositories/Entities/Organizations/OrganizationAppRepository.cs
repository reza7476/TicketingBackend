using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Ticketing.Application.Services.Entities.Organizations.Contracts;
using Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;
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

    public async Task<bool> ExistedById(string id)
    {
        return await _organizations.AnyAsync(_ => _.Id == id);
    }

    public async Task<bool> ExistedByOrganCode(string organCode)
    {
        return await _organizations.AnyAsync(o => o.OrganizationCode == organCode);
    }

    public async Task<List<GetAllOrganizationDto>> GetAll(string? search=null)
    {
        var query=  _organizations
            .Select(_ => new GetAllOrganizationDto()
            {
                Id = _.Id,
                Name = _.Name,
                IsDefault = _.IsDefault,
                OrganizationCode = _.OrganizationCode,
            }).AsQueryable();

        if(search != null)
        {
            search= search.Trim().ToLower();
            query=query.Where (s=>
            s.Name == search||
            s.OrganizationCode == search
            );
        }

        return await query.ToListAsync();

    }

    public async Task<Organization?> GetById(string id)
    {
        return await _organizations.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<bool> IsExistByOrganCode(string id, string organizationCode)
    {
        return await _organizations.AnyAsync(_=>_.Id!=id && _.OrganizationCode==organizationCode);  
    }
}
