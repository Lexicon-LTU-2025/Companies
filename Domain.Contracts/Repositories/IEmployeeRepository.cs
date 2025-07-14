using Domain.Models.Entities;

namespace Domain.Contracts.Repositories;
public interface IEmployeeRepository : IRepositoryBase<ApplicationUser>
{
    Task<IEnumerable<ApplicationUser>> GetEmployeesAsync(Guid companyId, bool trackChanges = false);
}