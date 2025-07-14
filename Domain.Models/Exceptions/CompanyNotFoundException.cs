namespace Domain.Models.Exceptions;

public class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(Guid id) : base($"The company with id: {id} was not found") { }
}
