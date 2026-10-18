using Ticketing.Application.Services.Entities.Roles.Contracts;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Application.UseCases.Users.Contracts;
using Ticketing.Application.UseCases.Users.Contracts.Dto;
using Ticketing.Domain.Entities.Users;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.UseCases.Users;

public class UserHandler : IUserHandler
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRoleService _roleService;
    private readonly IUnitOfWork _unitOfWork;



    public UserHandler(
        IUserService userService,
        IPasswordHasher passwordHasher,
        IRoleService roleService,
        IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _roleService = roleService;
        _unitOfWork = unitOfWork;
    }

    public async Task SignUpAgent(SignUpUserDto dto)
    {
        dto.Mobile = dto.Mobile.NormalizeIranPhoneNumber();
        var hashPass = _passwordHasher.GeneratePassword(dto.Password);
        await _unitOfWork.Begin();
        try
        {
            var roleId = await _roleService
                .GetRoleIdByRoleName(SystemRole.Agent);
            await _userService.Add(new AddUserDto()
            {
                FirstName = dto.Name,
                LastName = dto.LastName,
                Mobile = dto.Mobile,
                RoleId = roleId,
                Password = hashPass
            });

            await _unitOfWork.Commit();

        }
        catch (Exception)
        {
            await _unitOfWork.RoleBack();
            throw;
        }
    }

    public  async Task SignUpClient(SignUpUserDto dto)
    {
        dto.Mobile = dto.Mobile.NormalizeIranPhoneNumber();
        var hashPass = _passwordHasher.GeneratePassword(dto.Password);
        await _unitOfWork.Begin();
        try
        {
            var roleId = await _roleService
                .GetRoleIdByRoleName(SystemRole.Client);
            await _userService.Add(new AddUserDto()
            {
                FirstName = dto.Name,
                LastName = dto.LastName,
                Mobile = dto.Mobile,
                RoleId = roleId,
                Password = hashPass
            });

            await _unitOfWork.Commit();

        }
        catch (Exception)
        {
            await _unitOfWork.RoleBack();
            throw;
        }
    }
}
