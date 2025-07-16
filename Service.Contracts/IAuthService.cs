using Companis.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts;
public interface IAuthService
{
    Task<TokenDto> CreateTokenAsync(bool addTime);
    Task<TokenDto> RefreshTokenAsync(TokenDto token);
    Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
    Task<bool> ValidateUserAsync(UserAuthDto user);
}
