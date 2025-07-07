namespace Companies.API.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Position { get; set; } = null!;

    //FK
    public Guid CompanyId { get; set; }

    //Navigation property
    public Company Company { get; set; } = null!;
}
