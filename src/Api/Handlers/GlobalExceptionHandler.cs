#region

using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Api.Handlers;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionDetails = GetExceptionDetails(exception);
        var problemDetails = new ProblemDetails
        {
            Status = exceptionDetails.Status,
            Type = exceptionDetails.Type,
            Title = exceptionDetails.Title,
            Detail = exceptionDetails.Detail
        };
        if (exceptionDetails.Errors is not null)
        {
            problemDetails.Extensions["errors"] = exceptionDetails.Errors;
        }

        httpContext.Response.StatusCode = exceptionDetails.Status;
        httpContext.Response.ContentType = "application/problem+json";
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        logger.LogInformation(exception, "An exception of type {ExceptionType} was thrown. {ProblemDetails}",
            exception.GetType().Name, problemDetails);
        return true;
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            BadRequestException badRequestException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "BadRequest",
                "Bad request",
                badRequestException.Message,
                null),
            NotFoundException notFoundException => new ExceptionDetails(
                StatusCodes.Status404NotFound,
                "NotFound",
                "Not found",
                notFoundException.Message,
                null),
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validation error",
                "One or more validation errors has occurred",
                validationException.Errors),
            ForbiddenException forbiddenException => new ExceptionDetails(
                StatusCodes.Status403Forbidden,
                "Forbidden",
                "Forbidden resource",
                forbiddenException.Message,
                null),
            UnauthorizedException unauthorizedException => new ExceptionDetails(
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                "Unauthorized",
                unauthorizedException.Message,
                null),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Server error",
                "An unexpected error has occurred",
                null)
        };
    }

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}
