using AutoMapper;
using Companis.Shared;
using Domain.Contracts.Repositories;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;

    public EmployeeService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }
    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges = false)
    {
        var companyExists = await uow.CompanyRepository.GetCompanyAsync(companyId, trackChanges);
        if (companyExists is  null) return null!;

        var employees = await uow.EmployeeRepository.GetEmployeesAsync(companyId, trackChanges);
        return mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }
}
