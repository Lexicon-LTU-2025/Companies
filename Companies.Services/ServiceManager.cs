using Companies.Infractructure.Repositories;

namespace Companies.Services;

public class ServiceManager
{
    private Lazy<CompanyService> companyService;
    public CompanyService CompanyService => companyService.Value;
    //..
    //..
    //..

    public ServiceManager(IUnitOfWork uow)
    {
        companyService = new Lazy<CompanyService>(() => new CompanyService());
    }
}
