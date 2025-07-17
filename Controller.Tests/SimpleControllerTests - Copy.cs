using Companies.Presentation.TestDemosOnly;
using Companis.Shared.DTOs.CompanyDtos;
using Controller.Tests.Extensions;
using Controller.Tests.Fixtures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace Controller.Tests;

public class SimpleControllerTests2 : IClassFixture<DatabaseFixture>
{
    private SimpleController2 sut;
    private readonly DatabaseFixture fixture;

    //[Fact]
    //public async Task GetCompany_ShouldReturn_StatusCode200Ok()
    //{
    //    var sut = new SimpleController();

    //    var result = await sut.GetCompany();
    //    var resultType = result as OkResult;

    //    Assert.IsType<OkResult>(resultType);
    //    Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
    //}

    public SimpleControllerTests2(DatabaseFixture fixture)
    {
        this.fixture = fixture;
        sut = new SimpleController2(fixture.Context, fixture.Mapper);
    }

    [Fact]
    public async Task GetCompany_IsNotAuthenticated_ShouldReturn_401()
    {
        //Arrange
        sut.SetUserIsAuthenticated(false);

        //Act
        var result = await sut.GetCompany();
        //var resultType = result.Result as UnauthorizedResult;

        //Assert
        var resultType = Assert.IsType<UnauthorizedResult>(result.Result);
        Assert.Equal(StatusCodes.Status401Unauthorized, resultType.StatusCode);
    }

    [Fact]
    public async Task GetCompany_IsNotAuthenticated_ShouldReturn_401_Nr2()
    {
        //Arrange
        sut.SetUserIsAuthenticated(false);
        

        //Act
        var result = await sut.GetCompany();
        //var resultType = result.Result as UnauthorizedResult;

        //Assert
        var resultType = Assert.IsType<UnauthorizedResult>(result.Result);
        Assert.Equal(StatusCodes.Status401Unauthorized, resultType.StatusCode);
    }


    [Fact]
    public async Task GetCompany_IfAuthenticated_ShouldReturn_200Ok()
    {
        //Arrange
        sut.SetUserIsAuthenticated(true);

        //Act
        var result = await sut.GetCompany();
        var resultType = result.Result as OkObjectResult;

        //Assert
        Assert.IsType<OkObjectResult>(resultType);
        Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
    }


    [Fact]
    public async Task GetCompany2_ShouldReturn_200Ok()
    {
        //Arrange
        var actualNumberOfCompanies = fixture.Context.Companies.Count();
        sut.SetUserIsAuthenticated(true);

        //Act
        var result = await sut.GetCompany2();
        //var resultType = result.Result as OkObjectResult;

        //Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var t = ok.Value;
        var companiesFromSut = Assert.IsType<List<CompanyDto>>(ok.Value);
        Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
        Assert.Equal(companiesFromSut.Count, actualNumberOfCompanies);
    }




}
