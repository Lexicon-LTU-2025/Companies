namespace Domain.Models.Entities;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? Country { get; set; }

    //Navigation property
    public ICollection<ApplicationUser> Employees { get; set; } = [];
}
