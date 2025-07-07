namespace Companis.Shared;

public record CompanyDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; } 
    public string? Country { get; init; }
}
