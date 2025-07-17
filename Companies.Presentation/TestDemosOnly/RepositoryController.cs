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

    public RepositoryController(ICompanyRepository companyRepository)
    {
        this.companyRepository = companyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompany(bool includeEmployees = false)
    {
        var companies = await companyRepository.GetCompaniesAsync(includeEmployees);

        return Ok(companies);
    }
}
