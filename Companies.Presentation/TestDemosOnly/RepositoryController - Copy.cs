using AutoMapper;
using Companis.Shared.DTOs.CompanyDtos;
using Companis.Shared.Requests;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.TestDemosOnly;

[Route("api/repo2")]
[ApiController]
public class RepositoryController2 : ControllerBase
{
    private readonly ICompanyRepository companyRepository;
    private readonly IMapper mapper;
    private readonly IUserService userService;

    public RepositoryController2(ICompanyRepository companyRepository, IMapper mapper, IUserService userManager)
    {
        this.companyRepository = companyRepository;
        this.mapper = mapper;
        this.userService = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
    {
        var user = await userService.GetUserAsync(User);
        if(user is null) ArgumentNullException.ThrowIfNull(user);

        var companies = await companyRepository.GetCompaniesAsync(new CompanyRequestParameters());
        var dtos = mapper.Map<IEnumerable<CompanyDto>>(companies);

        return Ok(dtos);
    }
}
