
using Companis.Shared;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetCompaniesAsync(bool includeEmployees, bool trackChanges = false);
}