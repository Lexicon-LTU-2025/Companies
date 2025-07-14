using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companis.Shared.Responses;

public class ApiBaseResponse
{
    public bool Success { get; }
    protected ApiBaseResponse(bool success) => Success = success;

    public TResultType GetOkResult<TResultType>()
    {
        if (this is OkResponse<TResultType> apiOkResponse)
        {
            return apiOkResponse.Result;
        }

        throw new InvalidOperationException($"Response type {GetType().Name} is not OkRespone");
    }
}

public sealed class OkResponse<TResult> : ApiBaseResponse
{
    public TResult Result { get; }
    public OkResponse(TResult result) : base(true)
    {
        Result = result;
    }
}

public abstract class NotFoundResponse : ApiBaseResponse
{
    public string Message { get; }
    protected NotFoundResponse(string message) : base(false)
    {
        Message = message;
    }
}

public class CompanyNotFoundResponse : NotFoundResponse
{
    public CompanyNotFoundResponse(Guid id) : base($"Company with {id} is not found") { }
}


