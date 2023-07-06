using KingsUsers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KingsUsers.Attribute;

public class GlobalExceptionFilterAttribute : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Handle the exception and create a standardized error response
        var errorResponse = new ErrorResponse
        {
            Message = "An error occurred.",
            Error = context.Exception.Message
        };

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}