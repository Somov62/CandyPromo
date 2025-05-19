using ValidationException = CandyPromo.Server.Requests.Validation.ValidationException;

namespace CandyPromo.Server.Middlewares;

/// <summary>
/// Глобальный обработчик исключений,
/// который заворачивает все исключения в конверт.
/// </summary>
public class GlobalExceptionHandler(RequestDelegate next)
{
    /// <summary>
    /// Метод обработки исключений.
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(Envelope.Error(validationException.Errors));
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new EnvelopeInternalError(exception.Message, DateTime.Now));
        }
    }
}
