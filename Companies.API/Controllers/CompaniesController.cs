using AutoMapper;
using Companies.Infractructure.Repositories;
using Companis.Shared;
using Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CompaniesController : ControllerBase
    {
       // private readonly CompanyRepository repo;
       // private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public CompaniesController(IMapper mapper, IUnitOfWork uow)
        {
           // repo = new CompanyRepository(context);
            this.mapper = mapper;
            this.uow = uow;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees)
        {
            ////In Memory
            //var companies = await context.Companies.ToListAsync();
            //var demoDto = mapper.Map<IEnumerable<CompanyDto>>(companies);

            ////Project 1
            //var demoDto1 = await context.Companies.ProjectTo<CompanyDto>(mapper.ConfigurationProvider).ToListAsync();

            ////Project 2
            var dtos = includeEmployees ? mapper.Map<IEnumerable<CompanyDto>>(await uow.CompanyRepository.GetCompaniesAsync(true)) :
                                          mapper.Map<IEnumerable<CompanyDto>>(await uow.CompanyRepository.GetCompaniesAsync());

            //Select manual mapping
            //var dtos = await context.Companies.Select(c => new CompanyDto
            //                        {
            //                            Id = c.Id,
            //                            Name = c.Name,
            //                            Address = c.Address,
            //                            Country = c.Country
            //                        })
            //                        .ToListAsync();

            return Ok(dtos);
        }

       

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
        {
            var company = await uow.CompanyRepository.GetCompanyAsync(id);
            var dto = mapper.Map<CompanyDto>(company);

            if (dto == null) return NotFound();

            return Ok(dto);
        }




        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutCompany(Guid id, CompanyUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var existingCompany = await uow.CompanyRepository.GetCompanyAsync(id);

            if (existingCompany is null) return NotFound();

            mapper.Map(dto, existingCompany);

            await uow.CompleteAsync();

            //return Ok(mapper.Map<CompanyDto>(existingCompany)); //Just for demo!
            return NoContent();
        }

    

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompany(CompanyCreateDto dto)
        {
            //var company = new Company { Name = dto.Name, Address = dto.Address, Country = dto.Country };
            var company = mapper.Map<Company>(dto);

            uow.CompanyRepository.Create(company);
            await uow.CompleteAsync();

            var createdDto = mapper.Map<CompanyDto>(company);

            return CreatedAtAction(nameof(GetCompany), new { id = createdDto.Id }, createdDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await uow.CompanyRepository.GetCompanyAsync(id);
            if (company == null) return NotFound();

            uow.CompanyRepository.Delete(company);
            await uow.CompleteAsync();

            return NoContent();
        }

        //private bool CompanyExists(Guid id)
        //{
        //    return context.Companies.Any(e => e.Id == id);
        //}
    }
}
