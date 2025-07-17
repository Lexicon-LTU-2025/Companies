using Companis.Shared.DTOs.EmployeeDtos;

namespace Companis.Shared.DTOs.CompanyDtos;

public record CompanyDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }
    public IEnumerable<EmployeeDto>? Employees { get; init; }
}
