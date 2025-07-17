using Companis.Shared.DTOs.CompanyDtos;
using Companis.Shared.Requests;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetCompaniesAsync(CompanyRequestParameters requestParameters, bool includeEmployees, bool trackChanges = false);
    Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges = false);
}