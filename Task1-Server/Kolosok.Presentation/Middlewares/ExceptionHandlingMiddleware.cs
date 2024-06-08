using System.Text.Json;
using System.Text.Json.Serialization;
using Kolosok.Application.Validators;
using Kolosok.Domain.Exceptions;
using Kolosok.Domain.Exceptions.NotFound;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Kolosok.Presentation.Middlewares;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
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
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        var statusCode = StatusCodes.Status500InternalServerError;
        var errorMessage = "Internal Server Error";

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = StatusCodes.Status400BadRequest;
                errorMessage = badRequestException.Message;
                break;
            case NotFoundException notFoundException:
                statusCode = StatusCodes.Status404NotFound;
                errorMessage = notFoundException.Message;
                break;
            case ValidationException:
                statusCode = StatusCodes.Status400BadRequest;
                errorMessage = "Validation Error";
                break;
        }

        httpContext.Response.StatusCode = statusCode;

        var errorResponse = new
        {
            errorMessage = errorMessage,
            errors = exception is ValidationException validationException
                ? validationException.Errors.Select(error => new
                {
                    property = error.Key,
                    message = error.Value
                })
                : null
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
    }
}