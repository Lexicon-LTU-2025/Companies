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
public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{

    public CompanyRepository(ApplicationDbContext context) : base(context) { }
 

    public async Task<Company?> GetCompanyAsync(Guid id, bool trackChanges = false)
    {
        return await FindByCondition(c => c.Id == id, trackChanges).FirstOrDefaultAsync();
    }

    public async Task<List<Company>> GetCompaniesAsync(bool include = false, bool trackChanges = false)
    {
        return include ? await FindAll(trackChanges).Include(c => c.Employees).ToListAsync() :
                         await FindAll(trackChanges).ToListAsync();
    }
}
