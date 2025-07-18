﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companis.Shared.DTOs.CompanyDtos;

public record CompanyUpdateDto : CompanyManipulationDto
{
    [Required]
    public Guid Id { get; init; }
}
