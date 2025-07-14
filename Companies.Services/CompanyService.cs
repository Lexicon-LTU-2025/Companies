using AutoMapper;
using Companis.Shared;
using Domain.Contracts.Repositories;
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

    public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync(bool includeEmployees, bool trackChanges = false)
    {
        return mapper.Map<IEnumerable<CompanyDto>>(await uow.CompanyRepository.GetCompaniesAsync(includeEmployees, trackChanges));
    }

    public async Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges = false)
    {
        var company = await uow.CompanyRepository.GetCompanyAsync(id, trackChanges);

        if (company is null) throw new Exception("Aj då");

        return mapper.Map<CompanyDto>(company);
    }
}