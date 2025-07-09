using Domain.Models.Entities;

namespace Domain.Contracts.Repositories;
public interface ICompanyRepository
{
    Task<List<Company>> GetCompaniesAsync(bool include = false);
    Task<Company?> GetCompanyAsync(Guid id);
}