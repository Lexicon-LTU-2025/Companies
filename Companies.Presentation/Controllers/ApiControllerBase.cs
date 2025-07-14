using Companis.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Presentation.Controllers;
public class ApiControllerBase : ControllerBase
{
    [NonAction]
    public ActionResult ProcessError(ApiBaseResponse baseResponse)
    {
        return baseResponse switch
        {
            //NotFoundResponse => NotFound(), //Simple implementaiton
            NotFoundResponse => NotFound(new ProblemDetails() //More information
            {
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = ((NotFoundResponse)baseResponse).Message,
                Instance = HttpContext.Request.Path
            }),
            _ => throw new NotImplementedException()
            };

    }
}
