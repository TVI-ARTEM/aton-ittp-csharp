using System.Net;

namespace Users.API.ActionFilters;

public class ErrorResponse
{
    public ErrorResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public int StatusCode { get; }
    public string Message { get; }
}