using AutoMapper;
using Bogus;
using Companies.Infractructure.Data;
using Companies.Presentation.TestDemosOnly;
using Companis.Shared;
using Controller.Tests.Helpers;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Tests;
public class RepositoryControllerTests
{
    private Mock<ICompanyRepository> mockRepo;
    private RepositoryController sut;

    public RepositoryControllerTests()
    {
        mockRepo = new Mock<ICompanyRepository>();
        sut = new RepositoryController(mockRepo.Object, MapperFactory.Create());
    }

    [Fact]
    public async Task GetCompany_ShouldReturnAllCompanies()
    {
        //Arrange
        const int expectedCount = 2;
        var expectedCompanies = GetCompanies(expectedCount);
        mockRepo.Setup(x => x.GetCompaniesAsync(false, It.IsAny<bool>())).ReturnsAsync(expectedCompanies);

        //Act
        var result = await sut.GetCompany();

        var resultType = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
        var actualCompanies =  Assert.IsType<List<CompanyDto>>(resultType.Value);
        Assert.Equal(actualCompanies?.Count, expectedCount);


    }

    private List<Company> GetCompanies(int numberOfCompanies)
    {
        var faker = new Faker<Company>("sv").Rules((f, c) =>
        {
            c.Name = f.Company.CompanyName();
            c.Address = $"{f.Address.StreetAddress()}, {f.Address.City()}";
            c.Country = f.Address.Country();
        });

        return faker.Generate(numberOfCompanies);
    }
}
