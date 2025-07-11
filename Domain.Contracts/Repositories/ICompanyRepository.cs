using Domain.Models.Entities;

namespace Domain.Contracts.Repositories;
public interface ICompanyRepository : IRepositoryBase<Company>
{
    Task<List<Company>> GetCompaniesAsync(bool include = false, bool trackChanges = false);
    Task<Company?> GetCompanyAsync(Guid id, bool trackChanges = false);
}