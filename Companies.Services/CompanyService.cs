using Domain.Contracts.Repositories;
using Service.Contracts;

namespace Companies.Services;

public class CompanyService : ICompanyService
{
    private IUnitOfWork uow;

    public CompanyService(IUnitOfWork uow)
    {
        this.uow = uow;
    }
}