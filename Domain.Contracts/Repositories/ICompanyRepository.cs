using Companis.Shared.Requests;
using Domain.Models.Entities;

namespace Domain.Contracts.Repositories;
public interface ICompanyRepository : IRepositoryBase<Company>
{
    Task<List<Company>> GetCompaniesAsync(CompanyRequestParameters requestParameters, bool include = false, bool trackChanges = false);
    Task<Company?> GetCompanyAsync(Guid id, bool trackChanges = false);
}