using AutoMapper;
using Domain.Contracts.Repositories;
using Service.Contracts;

namespace Companies.Services;

public class ServiceManager : IServiceManager
{
    private Lazy<ICompanyService> companyService;
    private Lazy<IEmployeeService> employeeService;
    public ICompanyService CompanyService => companyService.Value;
    public IEmployeeService EmployeeService => employeeService.Value;

    //..
    //..
    //..

    public ServiceManager(Lazy<ICompanyService> companyService, Lazy<IEmployeeService> employeeService)
    {
        this.companyService = companyService;
        this.employeeService = employeeService;
    }
}
