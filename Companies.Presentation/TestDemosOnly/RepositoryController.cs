using AutoMapper;
using Companis.Shared.DTOs.CompanyDtos;
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

[Route("api/repo")]
[ApiController]
public class RepositoryController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;

    public RepositoryController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompany(bool includeEmployees = false)
    {
        var user = await userManager.GetUserAsync(User);
        if(user is null) ArgumentNullException.ThrowIfNull(user);

        var companies = await unitOfWork.CompanyRepository.GetCompaniesAsync(includeEmployees);
        var dtos = mapper.Map<IEnumerable<CompanyDto>>(companies);

        return Ok(dtos);
    }
}
