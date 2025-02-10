using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring JWT authentication in the application.
/// </summary>
public static class JwtAuthenticationExtensions
{
    /// <summary>
    /// Configures JWT-based authentication and authorization for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add authentication services to.</param>
    /// <param name="configuration">The application's configuration containing JWT settings.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> with authentication services configured.</returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection jwtSettings = configuration.GetSection("JwtSettings");

        string? secretKey = jwtSettings.GetValue<string>("SecretKey");
        string? issuer = jwtSettings.GetValue<string>("Issuer");
        string? audience = jwtSettings.GetValue<string>("Audience");


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                };
            });


        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
                policy.RequireRole("Admin"));
        });

        return services;
    }
}
