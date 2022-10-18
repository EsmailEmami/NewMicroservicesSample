using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Middlewares;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            var statusCode = MapStatusCode(e);
            var message = GetMessages(e);
            LogError(context, (int)statusCode, message);
            await HandleExceptionAsync(context, e, statusCode, message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, HttpStatusCode statusCode, string meesage)
    {
        var response = new
        {
            statusCode,
            message = meesage
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = (int)statusCode;

        await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }

    private HttpStatusCode MapStatusCode(Exception exception)
    {
        if (exception is AggregateException && exception.InnerException is ValidationException)
            return HttpStatusCode.BadRequest;


        return exception switch
        {
            ArgumentNullException => HttpStatusCode.BadRequest,
            EntityNotFoundException => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            ValidationException => HttpStatusCode.BadRequest,
            DuplicateNameException => HttpStatusCode.Conflict,
            ApplicationException => HttpStatusCode.BadRequest,
            AggregateException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

    }

    private string GetMessages(Exception exception)
    {
        if (exception.InnerException is ValidationException)
        {
            return exception.InnerException.Message;
        }

        return exception switch
        {
            ApplicationException applicationException => applicationException.Message,
            ArgumentNullException argumentNullException => argumentNullException.Message,
            EntityNotFoundException entityNotFoundException => entityNotFoundException.Message,
            UnauthorizedAccessException unauthorizedAccessException => unauthorizedAccessException.Message,
            DuplicateNameException duplicateNameException => duplicateNameException.Message,
            ValidationException validationException => validationException.Message,
            _ => "خطای سیستمی"
        };
    }

    private void LogError(HttpContext context, int statusCode, string message)
    {
        var logTitle = $"{context.Request.Path} :: [{statusCode}] {message}";
        var logError = new
        {
            Context = context,
        };

        if (statusCode >= 500)
        {
            _logger.LogCritical(logTitle, logError);
        }
        else if (statusCode == 401)
        {
            _logger.LogInformation(logTitle, logError);
        }
        else
        {
            _logger.LogWarning(logTitle, logError);
        }
    }
}