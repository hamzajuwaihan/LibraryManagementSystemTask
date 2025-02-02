using LibraryManagementSystem.Api.Routes;
using LibraryManagementSystem.Application;
using LibraryManagementSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddInfrastructure(builder.Configuration)
                .AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.MapAuthorsEndpoint();
app.MapBooksEndpoint();
app.MapBorrowersEndpoint();
app.MapLoansEndpoint();

app.Run();
