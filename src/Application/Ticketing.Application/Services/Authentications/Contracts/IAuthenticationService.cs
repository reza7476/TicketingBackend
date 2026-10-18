using Ticketing.Application.Services.Authentications.Contracts.Dto;
using Ticketing.Shared.DTOS;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Authentications.Contracts;

public interface IAuthenticationAppService : IService
{
    Task<GetJwtTokenDto> Login(LoginDto dto);
}
