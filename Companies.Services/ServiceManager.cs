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

    public ServiceManager(IUnitOfWork uow, IMapper mapper)
    {
        companyService = new Lazy<ICompanyService>(() => new CompanyService(uow, mapper));
    }
}
