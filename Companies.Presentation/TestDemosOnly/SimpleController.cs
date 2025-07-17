using Companis.Shared.DTOs.CompanyDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.TestDemosOnly;

[Route("api/simple")]
[ApiController]
public class SimpleController : ControllerBase
{
    public SimpleController()
    {

    }

    [HttpGet]
    public async Task<ActionResult<CompanyDto>> GetCompany()
    {
        if (User?.Identity?.IsAuthenticated ?? false)
        {
            return Ok("User is authenticated");
        }
        else
        {
            return Unauthorized();
        }
    }
}
