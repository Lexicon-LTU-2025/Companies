﻿using Companies.Presentation.TestDemosOnly;
using Controller.Tests.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace Controller.Tests;

public class SimpleControllerTests
{
    private SimpleController sut;

    //[Fact]
    //public async Task GetCompany_ShouldReturn_StatusCode200Ok()
    //{
    //    var sut = new SimpleController();

    //    var result = await sut.GetCompany();
    //    var resultType = result as OkResult;

    //    Assert.IsType<OkResult>(resultType);
    //    Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
    //}

    public SimpleControllerTests()
    {
        sut = new SimpleController();
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


}
