namespace Domain.Contracts.Repositories;
public interface IUnitOfWork
{
    ICompanyRepository CompanyRepository { get; }

    Task CompleteAsync();
}