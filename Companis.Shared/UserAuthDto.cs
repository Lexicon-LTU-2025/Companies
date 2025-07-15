using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companis.Shared;
public record UserAuthDto
{
    [Required]
    public string UserName { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}
