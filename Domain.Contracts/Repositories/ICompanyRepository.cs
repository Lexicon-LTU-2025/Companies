using Domain.Models.Entities;

namespace Domain.Contracts.Repositories;
public interface ICompanyRepository
{
    Task<List<Company>> GetCompaniesAsync(bool include = false, bool trackChanges = false);
    Task<Company?> GetCompanyAsync(Guid id, bool trackChanges = false);

    void Create(Company company);
    void Update(Company company);
    void Delete(Company company);   
}