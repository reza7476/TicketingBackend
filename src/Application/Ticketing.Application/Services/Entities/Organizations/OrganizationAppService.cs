using Ticketing.Application.Services.Entities.Organizations.Contracts;
using Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;
using Ticketing.Application.Services.Entities.Organizations.Exceptions;
using Ticketing.Domain.Entities.Organizations;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.Services.Entities.Organizations;
public class OrganizationAppService : IOrganizationService
{

    private readonly IOrganizationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public OrganizationAppService(
        IOrganizationRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Add(AddOrganizationDto dto)
    {
        if(await _repository.ExistedByName(dto.Name))
        {
            throw new OrganizationNameIsDuplicateException();
        }

        var organ = new Organization()
        {
            Name = dto.Name,
            Id = Guid.NewGuid().ToString()
        };
        await _repository.Add(organ);
        await _unitOfWork.Complete();
        return organ.Id;
    }
}
