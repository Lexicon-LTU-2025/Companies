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

    private readonly ApplicationDbContext context;

    public UnitOfWork(
        ApplicationDbContext context,
        Lazy<ICompanyRepository> companyRepository,
        Lazy<IEmployeeRepository> employeeRepository)
    {
        this.companyRepository =  companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        this.employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(UnitOfWork.employeeRepository));
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CompleteAsync() => await context.SaveChangesAsync();
}
