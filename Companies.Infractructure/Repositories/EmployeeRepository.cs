using Companies.Infractructure.Data;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Infractructure.Repositories;
public class EmployeeRepository : RepositoryBase<ApplicationUser>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<ApplicationUser>> GetEmployeesAsync(Guid companyId, bool trackChanges = false)
    {
        return await FindByCondition(e => e.CompanyId == companyId, trackChanges).ToListAsync();
    }
}
