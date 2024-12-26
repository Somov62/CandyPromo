using Microsoft.OpenApi.Models;

namespace CandyPromo.Server.ServiceCollectionExtensions;

/// <summary>
/// Расширения для <see cref="IServiceCollection"/>
/// чтобы добавить в проект swagger с авторизацией.
/// </summary>
public static class Swagger
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}
