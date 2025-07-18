using Companis.Shared.Requests;
using Domain.Models.Entities;

namespace Domain.Contracts.Repositories;
public interface ICompanyRepository : IRepositoryBase<Company>
{
    Task<PagedList<Company>> GetCompaniesAsync(CompanyRequestParameters requestParameters, bool trackChanges = false);
    Task<Company?> GetCompanyAsync(Guid id, bool trackChanges = false);
}