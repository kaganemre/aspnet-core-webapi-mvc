using System.Net;
using GuitarShopApp.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GuitarShopApp.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ExceptionFilter : ExceptionFilterAttribute
{
    public async override Task OnExceptionAsync(ExceptionContext context)
    {
        var statusCode = HttpStatusCode.InternalServerError;

        if (context.Exception is DataNotFoundException)
            statusCode = HttpStatusCode.NotFound;
            
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)statusCode;

        context.Result = new JsonResult(new
        {
            error = new[] { context.Exception.Message },
            statusCode = (int)statusCode,
            stackTrace = context.Exception.StackTrace
        });
    }
}