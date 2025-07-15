using Companis.Shared;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Services;
public class AuthService : IAuthService
{
    public Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
    {
        
    }
}
