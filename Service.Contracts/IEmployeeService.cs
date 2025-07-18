﻿using Companis.Shared.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts;
public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges = false);
}
