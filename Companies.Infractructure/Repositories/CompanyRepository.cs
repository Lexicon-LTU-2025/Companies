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
public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext context;

    public CompanyRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Company?> GetCompanyAsync(Guid id)
    {
        return await context.Companies.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Company>> GetCompaniesAsync(bool include = false)
    {
        return include ? await context.Companies.Include(c => c.Employees).ToListAsync() :
                         await context.Companies.ToListAsync();
    }
}
