using AutoMapper;
using Domain.Contracts.Repositories;
using Service.Contracts;

namespace Companies.Services;

public class ServiceManager : IServiceManager
{
    private Lazy<ICompanyService> companyService;
    public ICompanyService CompanyService => companyService.Value;
    //..
    //..
    //..

    public ServiceManager(Lazy<ICompanyService> companyService)
    {
        this.companyService = companyService;
    }
}
