using AutoMapper;
using Companies.Services;
using Companis.Shared;
using Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace Companies.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public CompaniesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees) =>
                 Ok(await serviceManager.CompanyService.GetCompaniesAsync(includeEmployees));

        //[HttpGet("{id:guid}")]
        //public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
        //{
        //    var company = await uow.CompanyRepository.GetCompanyAsync(id);
        //    var dto = mapper.Map<CompanyDto>(company);

        //    if (dto == null) return NotFound();

        //    return Ok(dto);
        //}

        //[HttpPut("{id:guid}")]
        //public async Task<IActionResult> PutCompany(Guid id, CompanyUpdateDto dto)
        //{
        //    if (id != dto.Id) return BadRequest();

        //    var existingCompany = await uow.CompanyRepository.GetCompanyAsync(id, trackChanges: true);

        //    if (existingCompany is null) return NotFound();

        //    mapper.Map(dto, existingCompany);

        //    await uow.CompleteAsync();

        //    //return Ok(mapper.Map<CompanyDto>(existingCompany)); //Just for demo!
        //    return NoContent();
        //}

        //[HttpPost]
        //public async Task<ActionResult<CompanyDto>> PostCompany(CompanyCreateDto dto)
        //{
        //    //var company = new Company { Name = dto.Name, Address = dto.Address, Country = dto.Country };
        //    var company = mapper.Map<Company>(dto);

        //    uow.CompanyRepository.Create(company);
        //    await uow.CompleteAsync();

        //    var createdDto = mapper.Map<CompanyDto>(company);

        //    return CreatedAtAction(nameof(GetCompany), new { id = createdDto.Id }, createdDto);
        //}

        //[HttpDelete("{id:guid}")]
        //public async Task<IActionResult> DeleteCompany(Guid id)
        //{
        //    var company = await uow.CompanyRepository.GetCompanyAsync(id);
        //    if (company == null) return NotFound();

        //    uow.CompanyRepository.Delete(company);
        //    await uow.CompleteAsync();

        //    return NoContent();
        //}

        //private bool CompanyExists(Guid id)
        //{
        //    return context.Companies.Any(e => e.Id == id);
        //}
    }
}
