using FluentValidation;
using LibraryManagementSystem.Api.Extensions;
using LibraryManagementSystem.Api.Middlewares;
using LibraryManagementSystem.Api.Routes;
using LibraryManagementSystem.Application;
using LibraryManagementSystem.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration)
                .AddApplication();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithJwt();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

WebApplication app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapAuthorsEndpoint();
app.MapBooksEndpoint();
app.MapBorrowersEndpoint();
app.MapLoansEndpoint();
app.MapUsersEndpoint();

app.Run();
