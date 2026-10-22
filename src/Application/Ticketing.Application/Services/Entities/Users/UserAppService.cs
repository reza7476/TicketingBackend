using Ticketing.Application.Services.Entities.Organizations.Contracts;
using Ticketing.Application.Services.Entities.Organizations.Exceptions;
using Ticketing.Application.Services.Entities.Roles.Contracts;
using Ticketing.Application.Services.Entities.Roles.Exceptions;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Application.Services.Entities.Users.Exceptions;
using Ticketing.Domain.Entities.UserRoles;
using Ticketing.Domain.Entities.Users;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.Services.Entities.Users;

public class UserAppService : IUserService
{

    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;
    private readonly IRoleRepository _roleRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserAppService(
        IUserRepository repository,
        IUnitOfWork unitOfWork,
        IDateTimeService dateTimeService,
        IRoleRepository roleRepository,
        IOrganizationRepository organizationRepository,
        IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
        _roleRepository = roleRepository;
        _organizationRepository = organizationRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Add(AddUserDto dto)
    {
        var role = await _roleRepository.GetById(dto.RoleId);
        if (role == null)
        {
            throw new RoleNotFoundException();
        }

        if (await _repository.IsExistByMobile(dto.Mobile))
        {
            throw new MobileIsDuplicateException();
        }
        var organ = await _organizationRepository.GetById(dto.OrganizationId);

        if (organ == null)
        {
            throw new OrganizationNotFoundException();
        }

        if (role.Title == SystemRole.Client && organ.IsDefault)
        {
            throw new SelectedOrganizationIsWrongException();
        }



        dto.Mobile = dto.Mobile.NormalizeIranPhoneNumber();
        var passwordHash = _passwordHasher.GeneratePassword(dto.Password);
        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            HashPassword = passwordHash,
            Mobile = dto.Mobile,
            CreatedAt = _dateTimeService.Now,
            OrganizationId = dto.OrganizationId
        };

        user.Roles.Add(new UserRole()
        {
            Id = Guid.NewGuid().ToString(),
            RoleId = dto.RoleId,
            UserId = user.Id
        });

        await _repository.Add(user);
        await _unitOfWork.Complete();
        return user.Id;
    }
}
