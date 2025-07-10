using Domain.Models.Entities;

namespace Domain.Contracts.Repositories;
public interface ICompanyRepository
{
    Task<List<Company>> GetCompaniesAsync(bool include = false);
    Task<Company?> GetCompanyAsync(Guid id);

    void Create(Company company);
    void Update(Company company);
    void Delete(Company company);   
}