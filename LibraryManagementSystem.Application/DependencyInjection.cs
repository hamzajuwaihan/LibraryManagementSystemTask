using LibraryManagementSystem.Application.Services.Authors;
using LibraryManagementSystem.Application.Services.Books;
using LibraryManagementSystem.Application.Services.Borrowers;
using LibraryManagementSystem.Application.Services.Jwt;
using LibraryManagementSystem.Application.Services.Loans;
using LibraryManagementSystem.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Application;

/// <summary>
/// Provides extension methods for configuring application-level dependencies.
/// </summary>
public static class DependencyInjection
{

    /// <summary>
    /// Adds application services to the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with registered application services.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<JwtTokenService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBorrowerService, BorrowerService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<IUserService, UserService>();


        return services;
    }
}
