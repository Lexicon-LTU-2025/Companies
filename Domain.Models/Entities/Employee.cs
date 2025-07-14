using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Entities;

public class Employee : IdentityUser
{
   // public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Position { get; set; } = null!;

    //FK
    public Guid CompanyId { get; set; }

    //Navigation property
    public Company Company { get; set; } = null!;
}
