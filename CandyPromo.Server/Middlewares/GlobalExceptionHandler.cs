using CandyPromo.Server.Controllers;
using CandyPromo.Server.Requests.Validation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CandyPromo.Server.Middlewares;

/// <summary>
/// Глобальный обработчик исключений,
/// который заворачивает все исключения в конверт.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(Envelope.Error(validationException.Errors), cancellationToken);
            return true;
        }
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(exception.Message, cancellationToken);
        return true;
    }
}
