using Companies.Presentation.TestDemosOnly;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Tests;

public class SimpleControllerTests
{
    [Fact]
    public async Task GetCompany_ShouldReturn_StatusCode200Ok()
    {
        var sut = new SimpleController();

        var result = await sut.GetCompany();
        var resultType = result as OkResult;

        Assert.IsType<OkResult>(resultType);
        Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
    }
}
