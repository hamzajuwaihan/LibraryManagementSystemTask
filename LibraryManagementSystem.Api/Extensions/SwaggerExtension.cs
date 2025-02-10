using Microsoft.OpenApi.Models;

namespace LibraryManagementSystem.Api.Extensions;

/// <summary>
/// Provides an extension method for configuring Swagger to support JWT authentication.
/// </summary>
public static class SwaggerExtension
{
    /// <summary>
    /// Configures Swagger to include JWT authentication support in the API documentation.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which Swagger services will be added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> with Swagger configured for JWT authentication.</returns>
    public static IServiceCollection AddSwaggerGenWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
        });

        return services;
    }
}
