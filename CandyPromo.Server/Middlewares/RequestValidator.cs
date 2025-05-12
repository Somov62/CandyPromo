using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text.Json;

namespace CandyPromo.Server.Middlewares;

/// <summary>
/// Реализация валидации запросов на основе middleware.
/// Аргумент конечной точки типа <see cref="IRequest"/>
/// будет автоматически провалидирован до входа в контроллер с помощью
/// вызова метода <see cref="IRequest.Validate"/>
/// </summary>
public class RequestValidator(RequestDelegate next)
{
    /// <summary>
    /// Опции для сериализации и десериализации тела запроса.
    /// </summary>
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    /// <summary>
    /// Выполняет логику middleware.
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        // Проверка наличия аргумента типа IRequest.
        var requestParameterInfo = GetRequestParameterInfo(context);

        if (requestParameterInfo == null)
        {
            await next(context);
            return;
        }

        // Получения данных из параметра.
        var request = await GetRequestParameterValue(context, requestParameterInfo);

        if (request == null)
        {
            await next(context);
            return;
        }

        // Валидация модели запроса.
        var validationSession = new ValidationSession();
        request.Validate(validationSession);

        if (validationSession.Errors.Count > 0)
        {
            // Ответ с ошибками валидации.
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(Envelope.Error(validationSession.Errors));
            return;
        }

        // Валидация пройдена, перезаписываем тело запроса на модифицированное.
        var json = JsonSerializer.Serialize(request, requestParameterInfo.ParameterType, _jsonOptions);
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(json));

        await next(context);
    }

    /// <summary>
    /// Вытаскивает с помощью рефлексии из конечного метода контроллера аргумент типа <see cref="IRequest"/>
    /// или возвращает null, если в методе нет такого аргумента.
    /// </summary>
    private ParameterInfo? GetRequestParameterInfo(HttpContext context)
    {
        // Получение маршрутных данных
        if (context.GetEndpoint() is not RouteEndpoint routeEndpoint)
            return null;

        return routeEndpoint.Metadata
            .GetMetadata<ControllerActionDescriptor>()?
            .MethodInfo?.GetParameters()
            .FirstOrDefault(p =>
                typeof(IRequest).IsAssignableFrom(p.ParameterType));
    }

    /// <summary>
    /// Читает данные из тела запроса и превращает их в сущность <see cref="IRequest"/>
    /// </summary>
    private async Task<IRequest?> GetRequestParameterValue(HttpContext context, ParameterInfo requestParameter)
    {
        // Извлечение значения параметра IRequest из запроса
        using var reader = new StreamReader(context.Request.Body);
        var requestBody = await reader.ReadToEndAsync();

        return JsonSerializer.Deserialize(requestBody, requestParameter.ParameterType, _jsonOptions) as IRequest;
    }
}
