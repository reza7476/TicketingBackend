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

  
}
