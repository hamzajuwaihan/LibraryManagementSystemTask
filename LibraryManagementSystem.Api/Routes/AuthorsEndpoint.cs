using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using LibraryManagementSystem.Api.Dtos.Authors;
using LibraryManagementSystem.Application.Services.Authors;
using LibraryManagementSystem.Domain.Authors.Entities;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Api.Routes;

public static class AuthorsEndpoint
{
    public static void MapAuthorsEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/authors").WithTags("Authors").RequireAuthorization();

        group.MapPost("/", async (HttpContext httpContext, IAuthorService authorService, AuthorRequestDto requestDto, IValidator<AuthorRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }
            string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            Author createdAuthor = await authorService.Create(new Author() { Bio = requestDto.Bio, Name = requestDto.Name, CreatedBy= Guid.Parse(userId) });

            return Results.Created($"/api/authors/{createdAuthor.Id}", createdAuthor);
        }).RequireAuthorization("Admin");

        group.MapGet("/", async (IAuthorService authorService, int limit = 10, int page = 1) =>
        {
            return Results.Ok(await authorService.GetAll(limit, page));
        });

        group.MapGet("/{id:guid}", async (IAuthorService authorService, Guid id) =>
        {
            return Results.Ok(await authorService.GetById(id));
        });

        group.MapDelete("/{id:guid}", async (IAuthorService authorService, Guid id) =>
        {
            await authorService.Delete(id);
            return Results.NoContent();
        });

        group.MapPut("/{id:guid}", async (HttpContext httpContext, IAuthorService authorService, Guid id, AuthorRequestDto requestDto, IValidator<AuthorRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }
            string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            return Results.Ok(await authorService.Update(id, new Author() {
                Bio = requestDto.Bio,
                Name = requestDto.Name,
                UpdatedBy = Guid.Parse(userId)
            }));
        });
    }
}
