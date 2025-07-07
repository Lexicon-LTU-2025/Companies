using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Companies.API.Data;
using Companies.API.Entities;
using AutoMapper;
using Companis.Shared;

namespace Companies.API.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EmployeesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee(Guid companyId)
        {
            var companyExists = await context.Companies.AnyAsync(c => c.Id.Equals(companyId));
            if(!companyExists) return NotFound();

            var employees = await context.Employees.Where(e => e.CompanyId.Equals(companyId)).ToListAsync();
            var employeeDtos = mapper.Map<IEnumerable<EmployeeDto>>(employees);

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

        //// POST: api/Employees
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    context.Employee.Add(employee);
        //    await context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        //}

        //// DELETE: api/Employees/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmployee(Guid id)
        //{
        //    var employee = await context.Employee.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    context.Employee.Remove(employee);
        //    await context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EmployeeExists(Guid id)
        //{
        //    return context.Employee.Any(e => e.Id == id);
        //}
    }
}
