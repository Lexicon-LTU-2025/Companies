using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companis.Shared.DTOs.EmployeeDtos;
public record EmployeeManipulationDto
{
    [Required(ErrorMessage = "Employee name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    [MinLength(2)]
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Age is a required field.")]
    [Range(18, 90)]
    public int Age { get; init; }

    [MaxLength(30, ErrorMessage = "Maximum length for the Position is 30 characters.")]
    public string? Position { get; init; }
}

public record EmployeeCreateDto : EmployeeManipulationDto { }
public record EmployeeUpdateDto : EmployeeManipulationDto { }
