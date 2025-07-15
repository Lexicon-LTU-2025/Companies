using AutoMapper;
using Companis.Shared;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
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
    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly ICompanyRepository companyRepository;

    public AuthService(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ICompanyRepository companyRepository
        )
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.companyRepository = companyRepository;
    }
    public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
    {
        ArgumentNullException.ThrowIfNull(userRegistrationDto);

        var roleExists = await roleManager.RoleExistsAsync(userRegistrationDto.Role);

        if (!roleExists)
            return IdentityResult.Failed(new IdentityError { Description = "Role does not exist" });

        //ToDo: Create AnyAsync wrapper in repo!
        var companyExists = await companyRepository.GetCompanyAsync(userRegistrationDto.CompanyId);

        if (companyExists is null)
            return IdentityResult.Failed(new IdentityError { Description = "Company does not exist" });

        var user = mapper.Map<ApplicationUser>(userRegistrationDto);

        var result = await userManager.CreateAsync(user, userRegistrationDto.Password);

        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, userRegistrationDto.Role);

        return result;
    }
}
