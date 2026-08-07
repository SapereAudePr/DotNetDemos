using System.Diagnostics;
using Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.ExceptionHandling;

public class AppExceptionHandler(
    IProblemDetailsService service,
    ILogger<AppExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception ex,
        CancellationToken ct)
    {
        if (ex is not AppException appException)
            return false;

        logger.LogWarning(ex,
            "An unhandled exception occurred {Id}",
            Activity.Current?.Id ?? context.TraceIdentifier);

        context.Response.StatusCode = appException.StatusCode;

        await service.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            ProblemDetails = new ProblemDetails
            {
                Detail = appException.Message,
                Status = appException.StatusCode
            }
        });

        return true;
    }
}