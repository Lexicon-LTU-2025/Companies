using Companis.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IServiceManager serviceManager;

    public AuthController(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserRegistrationDto userRegistrationDto)
    {
        IdentityResult result = await serviceManager.AuthService.RegisterUserAsync(userRegistrationDto);
    }
}
