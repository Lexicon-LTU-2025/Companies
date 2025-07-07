using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Companies.API.Data;
using Companies.API.Entities;
using Companis.Shared;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Companies.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CompaniesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany()
        {
            //In Memeory
            var companies = await context.Companies.ToListAsync();
            var demoDto = mapper.Map<IEnumerable<CompanyDto>>(companies);

            //Project 1
            var demoDto1 = await context.Companies.ProjectTo<CompanyDto>(mapper.ConfigurationProvider).ToListAsync();

            //Project 2
            var demoDto2 = await mapper.ProjectTo<CompanyDto>(context.Companies).ToListAsync();

            //Select manual mapping
            var dtos = await context.Companies.Select(c => new CompanyDto
                                    {
                                        Id = c.Id,
                                        Name = c.Name,
                                        Address = c.Address,
                                        Country = c.Country
                                    })
                                    .ToListAsync();

            return Ok(dtos);
        }

        //// GET: api/Companies/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Company>> GetCompany(Guid id)
        //{
        //    var company = await context.Companies.FindAsync(id);

        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    return company;
        //}

        //// PUT: api/Companies/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCompany(Guid id, Company company)
        //{
        //    if (id != company.Id)
        //    {
        //        return BadRequest();
        //    }

        //    context.Entry(company).State = EntityState.Modified;

        //    try
        //    {
        //        await context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CompanyExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Companies
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Company>> PostCompany(Company company)
        //{
        //    context.Companies.Add(company);
        //    await context.SaveChangesAsync();

        //    return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        //}

        //// DELETE: api/Companies/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCompany(Guid id)
        //{
        //    var company = await context.Companies.FindAsync(id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    context.Companies.Remove(company);
        //    await context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CompanyExists(Guid id)
        //{
        //    return context.Companies.Any(e => e.Id == id);
        //}
    }
}
