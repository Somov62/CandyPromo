using CandyPromo.Server.Controllers;
using CandyPromo.Server.Requests;
using CandyPromo.Server.Requests.Validation;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace CandyPromo.Server.Middlewares;

public class RequestValidator(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Проверка наличия аргумента типа IRequest
        var requestParameterInfo = GetRequestParameterInfo(context);

        if (requestParameterInfo == null)
        {
            await next(context);
            return;
        }

        var request = await GetRequestParameterValue(context, requestParameterInfo);

        if (request != null)
        {
            var validationSession = new ValidationSession();
            request.Validate(validationSession);
            if (validationSession.Errors.Count > 0)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(Envelope.Error(validationSession.Errors));
                return;
            }
        }

        await next(context);
    }

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

    private async Task<IRequest?> GetRequestParameterValue(HttpContext context, ParameterInfo requestParameter)
    {
        // Извлечение значения параметра IRequest из запроса
        context.Request.EnableBuffering();
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        return JsonSerializer.Deserialize(requestBody, requestParameter.ParameterType, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            
        }) as IRequest;
    }
}
