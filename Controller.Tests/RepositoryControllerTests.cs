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


public class RepositoryControllerTests
{
    private Mock<UserManager<ApplicationUser>> userManagerMock;
    private RepositoryController sut;
    private Mock<IUnitOfWork> mockUoW;
    private const int expectedCount = 2;

    public RepositoryControllerTests()
    {
      
        mockUoW = new Mock<IUnitOfWork>();
        mockUoW.Setup(x => x.CompanyRepository.GetCompaniesAsync(It.IsAny<CompanyRequestParameters>(), false)).ReturnsAsync(new PagedList<Company>(GetCompanies(expectedCount),10, 1, expectedCount)
            );

        //ToDo make helper method!
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        userManagerMock = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        sut = new RepositoryController(mockUoW.Object, MapperFactory.Create(), userManagerMock.Object);
    }

    [Fact]
    public async Task GetCompany_ShouldReturnAllCompanies()
    {
        //Arrange
        //var expectedCompanies = GetCompanies(expectedCount);
        //mockUoW.Setup(x => x.CompanyRepository.GetCompaniesAsync(false, It.IsAny<bool>())).ReturnsAsync(expectedCompanies);
        userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new ApplicationUser());

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
        userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(() => null);

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
