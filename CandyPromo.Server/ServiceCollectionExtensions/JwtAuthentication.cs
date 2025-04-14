using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CandyPromo.Server.ServiceCollectionExtensions;

/// <summary>
/// Расширение для <see cref="IServiceCollection"/>
/// чтобы добавить в проект Jwt аутентификацию одним методом.
/// </summary>
public static class JwtAuthentication
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Достаем из конфигурации ключ шифрования и срок жизни токена.
        var optionsSectionName = nameof(JwtOptions);
        var optionsSection = configuration.GetSection(optionsSectionName);

        services.Configure<JwtOptions>(optionsSection);
        var jwtOptions = optionsSection.Get<JwtOptions>();

        services
            // Регистрируем jwt библиотеку.
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)),
                    RoleClaimType = ClaimTypes.Role
                };
            });

        services.AddAuthorization();

        services.AddScoped<JwtTokenGenerator>();

        return services;
    }
}
