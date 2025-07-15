using AutoMapper;
using Domain.Contracts.Repositories;
using Service.Contracts;

namespace Companies.Services;

public class ServiceManager : IServiceManager
{
    private Lazy<ICompanyService> companyService;
    private Lazy<IEmployeeService> employeeService;
    private Lazy<IAuthService> authService;
    public ICompanyService CompanyService => companyService.Value;
    public IEmployeeService EmployeeService => employeeService.Value;
    public IAuthService AuthService => authService.Value;

    //..
    //..
    //..

    public ServiceManager(Lazy<ICompanyService> companyService, Lazy<IEmployeeService> employeeService, Lazy<IAuthService> authService)
    {
        this.companyService = companyService;
        this.employeeService = employeeService;
        this.authService = authService;
    }
}
