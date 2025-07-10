using Domain.Contracts.Repositories;

namespace Companies.Infractructure.Repositories;
public interface IUnitOfWork
{
    ICompanyRepository CompanyRepository { get; }

    Task CompleteAsync();
}