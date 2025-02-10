using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using LibraryManagementSystem.Domain.Users.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem.Application.Services.Jwt;

/// <summary>
/// Provides functionality for generating JWT tokens for user authentication.
/// </summary>
public class JwtTokenService
{
    /// <summary>
    /// The application configuration settings.
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// The secret key used for signing JWT tokens.
    /// </summary>
    private readonly string _secretKey;

    /// <summary>
    /// The issuer of the JWT tokens.
    /// </summary>
    private readonly string _issuer;

    /// <summary>
    /// The intended audience of the JWT tokens.
    /// </summary>
    private readonly string _audience;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenService"/> class.
    /// </summary>
    /// <param name="configuration">The application configuration containing JWT settings.</param>
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = _configuration["JwtSettings:SecretKey"]!;
        _issuer = _configuration["JwtSettings:Issuer"]!;
        _audience = _configuration["JwtSettings:Audience"]!;
    }

    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is being generated.</param>
    /// <returns>A signed JWT token as a string.</returns>
    public string GenerateJwtToken(User user)
    {
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("role", user.Role.ToString())
        ];

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
