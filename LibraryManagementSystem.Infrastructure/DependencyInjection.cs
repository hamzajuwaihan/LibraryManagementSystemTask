using LibraryManagementSystem.Domain.Authors.Interfaces;
using LibraryManagementSystem.Domain.Books.Interfaces;
using LibraryManagementSystem.Domain.Borrowers.Interfaces;
using LibraryManagementSystem.Domain.Loans.Interfaces;
using LibraryManagementSystem.Domain.Users.Interfaces;
using LibraryManagementSystem.Infrastructure.Database;
using LibraryManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        services.AddDbContext<LibraryManagementDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBorrowerRepository, BorrowerRepository>();

        return services;
    }
}
