using Companies.Presentation.TestDemosOnly;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Controller.Tests;

public class SimpleControllerTests
{
    //[Fact]
    //public async Task GetCompany_ShouldReturn_StatusCode200Ok()
    //{
    //    var sut = new SimpleController();

    //    var result = await sut.GetCompany();
    //    var resultType = result as OkResult;

    //    Assert.IsType<OkResult>(resultType);
    //    Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
    //}

    [Fact]
    public async Task GetCompany_IsNotAuthenticated_ShouldReturn_401()
    {
        //Arrange
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(false);

        var controllerContext = new ControllerContext()
        {
            HttpContext = mockHttpContext.Object
        };

        var sut = new SimpleController();
        sut.ControllerContext = controllerContext;

        //Act
        var result = await sut.GetCompany();
        var resultType = result as UnauthorizedResult;

        //Assert
        Assert.IsType<UnauthorizedResult>(resultType);
        Assert.Equal(StatusCodes.Status401Unauthorized, resultType.StatusCode);
    }
}
