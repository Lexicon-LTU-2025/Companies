namespace Domain.Contracts.Repositories;
public interface IUnitOfWork
{
    ICompanyRepository CompanyRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }

    Task CompleteAsync();
}