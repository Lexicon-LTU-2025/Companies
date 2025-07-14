using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Companies.API.Services;

public class DataSeedHostingService : IHostedService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DataSeedHostingService> logger;
    private UserManager<ApplicationUser> userManager = null!;
    private RoleManager<IdentityRole> roleManager = null!;
    private const string EmployeeRole = "Employee";
    private const string AdminRole = "Admin";

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

        userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //Null checks :)
        ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));
        ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

        try
        {
            await CreateRolesAsync(new[] {AdminRole, EmployeeRole});
            IEnumerable<Company> companies = GenerateCompanies(10);
            await context.Companies.AddRangeAsync(companies, cancellationToken);
            await GenerateEmployees(30, companies.ToList());
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Seed complete");
        }
        catch (Exception ex)
        {
            logger.LogError($"Data seed fail with error: {ex.Message}");
            throw;
        }
    }

    private async Task CreateRolesAsync(string[] rolenames)
    {
        foreach (string rolename in rolenames)
        {
            if(await roleManager.RoleExistsAsync(rolename)) continue;
            var role = new IdentityRole { Name = rolename };
            var res = await roleManager.CreateAsync(role);

            if(!res.Succeeded) throw new Exception(string.Join("\n", res.Errors));
        }
    }

    private IEnumerable<Company> GenerateCompanies(int numberOfCompanies)
    {
        var faker = new Faker<Company>("sv").Rules((f, c) =>
        {
            c.Name = f.Company.CompanyName();
            c.Address = $"{f.Address.StreetAddress()}, {f.Address.City()}";
            c.Country = f.Address.Country();
        });

        return faker.Generate(numberOfCompanies);
    }

    private async Task GenerateEmployees(int numberOfEmplyees, List<Company> companies)
    {
        string[] positions = ["Developer", "Tester", "Manager"];

        var faker = new Faker<ApplicationUser>("sv").Rules((f, e) =>
        {
            e.Name = f.Person.FullName;
            e.Age = f.Random.Int(min: 18, max: 70);
            e.Position = positions[f.Random.Int(0, positions.Length - 1)];
            e.Email = f.Person.Email;
            e.UserName = f.Person.UserName;
            e.Company = companies[f.Random.Int(0, companies.Count - 1)];
        });

        var employees = faker.Generate(numberOfEmplyees);

        foreach (var user in employees)
        {
            var result = await userManager.CreateAsync(user, "password");
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            if (user.Position == "Manager") await userManager.AddToRoleAsync(user, AdminRole);
            else await userManager.AddToRoleAsync(user, EmployeeRole);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

}
