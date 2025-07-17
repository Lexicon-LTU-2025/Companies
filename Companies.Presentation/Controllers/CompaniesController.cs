using Companis.Shared.DTOs.CompanyDtos;
using Companis.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Companies.Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    [EnableCors("AllowAll")]
    //[Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public CompaniesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
      //  [AllowAnonymous]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(
            bool includeEmployees,
            [FromQuery] CompanyRequestParameters companyRequestParams) =>
                 Ok(await serviceManager.CompanyService.GetCompaniesAsync(companyRequestParams, includeEmployees));

        [HttpGet("{id:guid}")]
        [Authorize(Policy = "CanEdit")] 
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid id) =>
                Ok(await serviceManager.CompanyService.GetCompanyAsync(id));
        

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
