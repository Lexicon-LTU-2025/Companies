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

    private readonly Lazy<ICompanyRepository> companyRepository;
    private readonly Lazy<IEmployeeRepository> employeeRepository;
    public ICompanyRepository CompanyRepository => companyRepository.Value;
    public IEmployeeRepository EmployeeRepository => employeeRepository.Value;
    //..
    //..
    //..
    //..
    //..
    //..
    //..
    // more repos

    private readonly ApplicationDbContext context;

    public UnitOfWork(ApplicationDbContext context)
    {
        companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(context));
        employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
        this.context = context;
    }

    public async Task CompleteAsync() => await context.SaveChangesAsync();
}
