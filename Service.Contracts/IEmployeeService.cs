using Companis.Shared;
using Companis.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts;
public interface IEmployeeService
{
    Task<ApiBaseResponse> GetEmployeesAsync(Guid companyId, bool trackChanges = false);
}
