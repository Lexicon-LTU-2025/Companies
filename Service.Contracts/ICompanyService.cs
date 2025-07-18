using Companis.Shared.DTOs.CompanyDtos;
using Companis.Shared.Requests;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<(IEnumerable<CompanyDto> companyDtos, MetaData metaData)> GetCompaniesAsync(CompanyRequestParameters requestParameters, bool trackChanges = false);
    Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges = false);
}