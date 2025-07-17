using AutoMapper;
using Bogus;
using Companies.Infractructure.Data;
using Companies.Presentation.TestDemosOnly;
using Companis.Shared.DTOs.CompanyDtos;
using Companis.Shared.Requests;
using Controller.Tests.Helpers;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Tests;
public class RepositoryControllerTests2
{
    private Mock<ICompanyRepository> mockRepo;
    private Mock<IUserService> userServiceMock;
    private RepositoryController2 sut;

    public RepositoryControllerTests2()
    {
        mockRepo = new Mock<ICompanyRepository>();

       // var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
       // userManagerMock = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        userServiceMock = new Mock<IUserService>();

        sut = new RepositoryController2(mockRepo.Object, MapperFactory.Create(), userServiceMock.Object);
    }

    [Fact]
    public async Task GetCompany_ShouldReturnAllCompanies()
    {
        //Arrange
        const int expectedCount = 2;
        var expectedCompanies = GetCompanies(expectedCount);
        mockRepo.Setup(x => x.GetCompaniesAsync(It.IsAny<CompanyRequestParameters>(),false, It.IsAny<bool>())).ReturnsAsync(expectedCompanies);
        userServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new ApplicationUser());

        //Act
        var result = await sut.GetCompany();

        var resultType = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
        var actualCompanies =  Assert.IsType<List<CompanyDto>>(resultType.Value);
        Assert.Equal(actualCompanies?.Count, expectedCount);


    }

    [Fact]
    public async Task GetCompany_ShouldThrow_Exception()
    {
        //Arrange
        userServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(() => null);

        //Act
        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.GetCompany());
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
