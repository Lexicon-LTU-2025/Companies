using System.ComponentModel.DataAnnotations;

namespace Companis.Shared;

public record CompanyManipulationDto
{
    [Required(ErrorMessage = "Company name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    [MinLength(2, ErrorMessage = "Minimin length for the Name is 2 characters")]
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Company address is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
    [MinLength(5, ErrorMessage = "Minimin length for the Address is 5 characters")]
    public string Address { get; init; } = string.Empty;

    [MaxLength(30, ErrorMessage = "Maximum length for the Country is 30 characters")]
    public string? Country { get; init; }
}

