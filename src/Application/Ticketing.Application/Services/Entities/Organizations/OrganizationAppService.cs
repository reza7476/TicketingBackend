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
        if (await _repository.ExistedByOrganCode(dto.OrganizationCode))
        {
            throw new OrganizationNameIsDuplicateException();
        }

        var organ = new Organization()
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            IsDefault = dto.IsDefault,
            OrganizationCode = dto.OrganizationCode,
        };
        await _repository.Add(organ);
        await _unitOfWork.Complete();
        return organ.Id;
    }

    public async Task Edit(EditOrganizationDto dto, string id)
    {
        var organ = await _repository.GetById(id);
        if (organ == null)
        {
            throw new OrganizationNotFoundException();
        }

        if (await _repository.IsExistByOrganCode(id, dto.OrganizationCode))
        {
            throw new OrganizationNameIsDuplicateException();
        }

        organ.OrganizationCode = dto.OrganizationCode;
        organ.Name = dto.Name;
        organ.IsDefault = dto.IsDefault;

        await _unitOfWork.Complete();   

    }

    public async Task<List<GetAllOrganizationDto>> GetAll(string? search = null)
    {
        return await _repository.GetAll(search);
    }
}
