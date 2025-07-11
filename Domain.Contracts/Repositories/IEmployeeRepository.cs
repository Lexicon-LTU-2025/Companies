using Domain.Models.Entities;

namespace Domain.Contracts.Repositories;
public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges = false);
}