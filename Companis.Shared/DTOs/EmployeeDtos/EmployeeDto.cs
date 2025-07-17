using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companis.Shared.DTOs.EmployeeDtos;
public record EmployeeDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; } 
    public required int Age { get; init; }
    public required string Position { get; init; } 
}
