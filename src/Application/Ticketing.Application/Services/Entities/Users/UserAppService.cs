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

    public UserAppService(
        IUserRepository repository,
        IUnitOfWork unitOfWork,
        IDateTimeService dateTimeService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task Add(AddUserDto dto)
    {
        if(await _repository.IsExistByMobile(dto.Mobile))
        {
            throw new MobileIsDuplicateException();
        }

        var user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            HashPassword = dto.Password,
            Id = Guid.NewGuid().ToString(),
            Mobile = dto.Mobile,
            CreatedAt = _dateTimeService.Now
        };

        user.Roles.Add(new UserRole()
        {
            Id = Guid.NewGuid().ToString(),
            RoleId = dto.RoleId,
            UserId = user.Id
        });

        await _repository.Add(user);
        await _unitOfWork.Complete();
    }
}
