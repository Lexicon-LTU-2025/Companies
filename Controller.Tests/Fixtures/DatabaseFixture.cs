using AutoMapper;
using Bogus;
using Companies.Infractructure.Data;
using Controller.Tests.Helpers;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Tests.Fixtures;
public class DatabaseFixture : IDisposable
{
    public IMapper Mapper { get; }
    public ApplicationDbContext Context { get; }

    public DatabaseFixture()
    {
        Mapper = MapperFactory.Create();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
         .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDataBase;Trusted_Connection=True;MultipleActiveResultSets=true")
         .Options;

        var context = new ApplicationDbContext(options);

        context.Database.Migrate();
        context.Companies.AddRange(GetCompanys(10));

        context.SaveChanges();
        Context = context;

    }

    private List<Company> GetCompanys(int numberOfCompanies)
    {
        var faker = new Faker<Company>("sv").Rules((f, c) =>
        {
            c.Name = f.Company.CompanyName();
            c.Address = $"{f.Address.StreetAddress()}, {f.Address.City()}";
            c.Country = f.Address.Country();
        });

        return faker.Generate(numberOfCompanies);

    }
    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
