using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace KingsUsers.Models;

public class ErrorDetails : ProblemDetails
{
    public ErrorDetails(Exception ex)
    {
        Title = "An error occurred";
        Status = (int)HttpStatusCode.InternalServerError;
        Detail = ex.Message;
        Type = ex.GetType().ToString();
    }
}