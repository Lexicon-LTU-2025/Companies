using AutoMapper;
using Companis.Shared.DTOs.CompanyDtos;
using Companis.Shared.Requests;
using Domain.Contracts.Repositories;
using Domain.Models.Exceptions;
using Service.Contracts;

namespace Companies.Services;

public class CompanyService : ICompanyService
{
    private IUnitOfWork uow;
    private readonly IMapper mapper;

    public CompanyService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    public async Task<(IEnumerable<CompanyDto> companyDtos, MetaData metaData)> GetCompaniesAsync(CompanyRequestParameters requestParameters, bool trackChanges = false)
    {
        var commpaniesWithMetaData = await uow.CompanyRepository.GetCompaniesAsync(requestParameters, trackChanges);
        var companyDtos = mapper.Map<IEnumerable<CompanyDto>>(commpaniesWithMetaData.Items);
        return (companyDtos, commpaniesWithMetaData.MetaData);
    }

    public async Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges = false)
    {
        var company = await uow.CompanyRepository.GetCompanyAsync(id, trackChanges);

        if (company is null) throw new CompanyNotFoundException(id);

        return mapper.Map<CompanyDto>(company);
    }
}