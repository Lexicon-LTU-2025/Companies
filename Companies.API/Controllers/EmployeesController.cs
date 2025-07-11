using AutoMapper;
using Companis.Shared;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace Companies.API.Controllers
{
    [Route("api/companies/{companyId:guid}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public EmployeesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee(Guid companyId)
        {

            var employeeDtos = await serviceManager.EmployeeService.GetEmployeesAsync(companyId);


           

            return Ok(employeeDtos);
        }

        //// GET: api/Employees/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        //{
        //    var employee = await context.Employee.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        //// PUT: api/Employees/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        //{
        //    if (id != employee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
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

        //[HttpPatch("{id:guid}")]
        //public async Task<IActionResult> PatchEmployee(Guid companyId, Guid id, JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        //{
        //    if (patchDocument is null) return BadRequest(); //Use Problem();

        //    var companyExists = await context.Companies
        //                                  .AnyAsync(c => c.Id == companyId);

        //    if (!companyExists) return Problem(
        //         statusCode: StatusCodes.Status404NotFound,
        //         title: "Company not found",
        //         detail: $"Company with id:{companyId} could not be located"
        //        );

        //    var employeeToPatch = await context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.CompanyId == companyId);

        //    if (employeeToPatch is null) return NotFound(); //Use Problem();

        //    var employeeToPatchDto = mapper.Map<EmployeeUpdateDto>(employeeToPatch);

        //    patchDocument.ApplyTo(employeeToPatchDto, ModelState);
        //    TryValidateModel(employeeToPatchDto);

        //    if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

        //    mapper.Map(employeeToPatchDto, employeeToPatch);
        //    await context.SaveChangesAsync();

        //    return NoContent();
        //}

        //// POST: api/Employees
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    context.Employee.Add(employee);
        //    await context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        //}

        //[HttpDelete("{id:guid}")]
        //public async Task<IActionResult> DeleteEmployee(Guid companyId, Guid id)
        //{
        //    var companyExists = await context.Companies
        //                                  .AnyAsync(c => c.Id == companyId);

        //    if (!companyExists) return Problem(
        //         statusCode: StatusCodes.Status404NotFound,
        //         title: "Company not found",
        //         detail: $"Company with id:{companyId} could not be located"
        //        );

        //    var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.CompanyId == companyId);

        //    if (employee == null) return NotFound();

        //    context.Employees.Remove(employee);
        //    await context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EmployeeExists(Guid id)
        //{
        //    return context.Employee.Any(e => e.Id == id);
        //}
    }
}
