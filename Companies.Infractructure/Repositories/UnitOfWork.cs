using Companies.Infractructure.Data;
using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Infractructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    public ICompanyRepository CompanyRepository { get; }
    //..
    // more repos

    private readonly ApplicationDbContext context;

    public UnitOfWork(ApplicationDbContext context)
    {
        CompanyRepository = new CompanyRepository(context);
        this.context = context;
    }

    public async Task CompleteAsync() => await context.SaveChangesAsync();
}
