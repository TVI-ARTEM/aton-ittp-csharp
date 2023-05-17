using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Users.API.ActionFilters;

public sealed class ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException exception:
                HandleBadRequest(context, exception);
                return;
            case ArgumentNullException exception:
                HandleNotFound(context, exception);
                return;
            case ArgumentException exception:
                HandleBadRequest(context, exception);
                return;
            default:
                HandlerInternalError(context);
                return;
        }
    }

    private static void HandlerInternalError(ExceptionContext context)
    {
        var jsonResult = new JsonResult(new ErrorResponse(
            (int)HttpStatusCode.InternalServerError,
            "Server Error!"))
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
        context.Result = jsonResult;
    }

    private static void HandleBadRequest(ExceptionContext context, Exception exception)
    {
        var jsonResult = new JsonResult(
            new ErrorResponse(
                (int)HttpStatusCode.BadRequest,
                exception.Message))
        {
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        context.Result = jsonResult;
    }

    private static void HandleNotFound(ExceptionContext context, Exception exception)
    {
        var jsonResult = new JsonResult(
            new ErrorResponse(
                (int)HttpStatusCode.NotFound,
                exception.Message))
        {
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.Result = jsonResult;
    }
}