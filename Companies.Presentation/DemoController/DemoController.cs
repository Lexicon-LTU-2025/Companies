using Companies.Infractructure.Data;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.DemoController;

[Route("api/demo")]
[ApiController]
[Authorize]
public class UserDemoController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly UserManager<ApplicationUser> userManager;

    public UserDemoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Demo()
    {
        var userName = userManager.GetUserName(User);
        var userName2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        var userWithRoleAdmin = await userManager.GetUsersInRoleAsync("Admin");
        var user = await userManager.GetUserAsync(User);
        var userId = userManager.GetUserId(User);
        var userId2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if(User.Identity.IsAuthenticated)
        {
            //..
        }

        return Ok();
    }
}
