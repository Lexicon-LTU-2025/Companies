using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Services;

public class DataSeedHostingService : IHostedService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DataSeedHostingService> logger;

    public DataSeedHostingService(IServiceProvider serviceProvider, ILogger<DataSeedHostingService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        if (!env.IsDevelopment()) return;

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (await context.Companies.AnyAsync(cancellationToken)) return;

        try
        {
            IEnumerable<Company> companies = GenerateCompanies(10);
            await context.Companies.AddRangeAsync(companies, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Seed complete");
        }
        catch (Exception ex)
        {
            logger.LogError($"Data seed fail with error: {ex.Message}");
            throw;
        }
    }

    private IEnumerable<Company> GenerateCompanies(int numberOfCompanies)
    {
        var faker = new Faker<Company>("sv").Rules((f, c) =>
        {
            c.Name = f.Company.CompanyName();
            c.Address = $"{f.Address.StreetAddress()}, {f.Address.City()}";
            c.Country = f.Address.Country();
            c.Employees = GenerateEmployees(f.Random.Int(min: 2, max: 10));
        });

        return faker.Generate(numberOfCompanies);
    }

    private static List<Employee> GenerateEmployees(int numberOfEmplyees)
    {
        string[] positions = ["Developer", "Tester", "Manager"];

        var faker = new Faker<Employee>("sv").Rules((f, e) =>
        {
            e.Name = f.Person.FullName;
            e.Age = f.Random.Int(min: 18, max: 70);
            e.Position = positions[f.Random.Int(0, positions.Length - 1)];
        });

        return faker.Generate(numberOfEmplyees);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

}
