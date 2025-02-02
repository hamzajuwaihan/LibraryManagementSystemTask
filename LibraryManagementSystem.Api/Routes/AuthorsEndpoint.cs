using LibraryManagementSystem.Api.Dtos.Authors;
using LibraryManagementSystem.Application.Services.Authors;
using LibraryManagementSystem.Domain.Authors.Entities;

namespace LibraryManagementSystem.Api.Routes;

public static class AuthorsEndpoint
{
    public static void MapAuthorsEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/authors").WithTags("Authors"); ;

        group.MapPost("/", async (IAuthorService authorService, CreateAuthorRequestDto author) =>
        {
            Author createdAuthor = await authorService.Create(new Author() { Bio = author.Bio, Name = author.Name });
            return Results.Created($"/api/authors/{createdAuthor.Id}", createdAuthor);
        });

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

        group.MapPut("/{id:guid}", async (IAuthorService authorService, Guid id, UpdateAuthorRequestDto requestDto) =>
        {
            
            return Results.Ok(await authorService.Update(id, new Author() {
                Bio = requestDto.Bio,
                Name = requestDto.Name
            }));
        });
    }
}
