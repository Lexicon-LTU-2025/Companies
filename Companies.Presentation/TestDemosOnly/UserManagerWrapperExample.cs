using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.TestDemosOnly;
internal class UserManagerWrapperExample
{
}

public interface IUserService
{
    Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal principal);
    Task<bool> IsInRoleAsync(ApplicationUser user, string role);
}

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }


    public async Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal principal)
    {
        return await userManager.GetUserAsync(principal);
    }

    public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
    {
        return await userManager.IsInRoleAsync(user, role);
    }
}

