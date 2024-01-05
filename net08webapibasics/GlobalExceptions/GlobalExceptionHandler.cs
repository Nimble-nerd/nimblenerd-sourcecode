using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net08apibasics.Features.NewProject.Exceptions;

namespace net08apibasics.GlobalExceptions;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Title = "Server error"
        };
        if (exception is ProjectNotFoundException)
        {
            problemDetails.Status = StatusCodes.Status404NotFound;
            httpContext.Response.StatusCode = problemDetails.Status.Value;
        }
        if (exception is ValidationException)
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = exception.ToString();
            httpContext.Response.StatusCode = problemDetails.Status.Value;
        }

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}