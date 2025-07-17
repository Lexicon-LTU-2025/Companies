using AutoMapper;
using Companies.Infractructure.Data;
using Companies.Infractructure.Repositories;
using Companis.Shared.DTOs.CompanyDtos;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.TestDemosOnly;

[Route("api/simple2")]
[ApiController]
public class SimpleController2 : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public SimpleController2(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompany2()
    {
        var companies = await context.Companies.ToListAsync();
        var dtos = mapper.Map<IEnumerable<CompanyDto>>(companies);
        return Ok(dtos);
    }

}
