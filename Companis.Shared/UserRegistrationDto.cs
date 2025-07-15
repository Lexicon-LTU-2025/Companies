using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companis.Shared;
public record UserRegistrationDto : EmployeeManipulationDto
{
    [Required]
    public Guid CompanyId { get; init; }
    
    [Required]
    public string Password { get; init; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;
   
    [Required]
    public string UserName { get; init; } = string.Empty;
    
    [Required]
    public string Role { get; init; } = string.Empty;
}
