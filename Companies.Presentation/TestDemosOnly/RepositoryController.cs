using AutoMapper;
using Companis.Shared;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
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
    private readonly ICompanyRepository companyRepository;
    private readonly IMapper mapper;

    public RepositoryController(ICompanyRepository companyRepository, IMapper mapper)
    {
        this.companyRepository = companyRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompany(bool includeEmployees = false)
    {
        var companies = await companyRepository.GetCompaniesAsync(includeEmployees);
        var dtos = mapper.Map<IEnumerable<CompanyDto>>(companies);

        return Ok(dtos);
    }
}
