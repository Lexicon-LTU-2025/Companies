using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Exceptions;
public class TokenValidationException : Exception
{
    public int StatusCode { get; }
    public TokenValidationException(string message, int statusCode = 401)
        : base(message) => StatusCode = statusCode;
}
