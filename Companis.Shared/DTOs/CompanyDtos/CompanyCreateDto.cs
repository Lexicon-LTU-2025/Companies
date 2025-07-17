using Companis.Shared.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companis.Shared.DTOs.CompanyDtos;


public record CompanyCreateDto : CompanyManipulationDto 
{
    public IEnumerable<EmployeeCreateDto>? Employees { get; init; }
}

