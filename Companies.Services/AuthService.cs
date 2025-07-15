using AutoMapper;
using Companis.Shared;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Services;
public class AuthService : IAuthService
{
    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly ICompanyRepository companyRepository;
    private readonly IConfiguration configuration;
    private ApplicationUser? user;

    public AuthService(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ICompanyRepository companyRepository,
        IConfiguration configuration
        )
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.companyRepository = companyRepository;
        this.configuration = configuration;
    }

    public async Task<string> CreateTokenAsync()
    {
        SigningCredentials signing = GetSigningCredentials();
        IEnumerable<Claim> claims = await GetClaimsAsync();
        JwtSecurityToken token = GenerateToken(signing, claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GenerateToken(SigningCredentials signing, IEnumerable<Claim> claims)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");

        var token = new JwtSecurityToken(
                                    issuer: jwtSettings["Issuer"],
                                    audience: jwtSettings["Audience"],
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
                                    signingCredentials: signing);

        return token;
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        ArgumentNullException.ThrowIfNull(user);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim("Age", user.Age.ToString())
            //Add more if needed
        };

        var roles = await userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;

    }

    private SigningCredentials GetSigningCredentials()
    {
        var secretKey = configuration["secretkey"];
        ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));

        var key = Encoding.UTF8.GetBytes(secretKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
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

    public async Task<bool> ValidateUserAsync(UserAuthDto userDto)
    {
        ArgumentNullException.ThrowIfNull(userDto);

        user = await userManager.FindByNameAsync(userDto.UserName);

        return user != null && await userManager.CheckPasswordAsync(user, userDto.Password);
    }
}
