using LibraryManagementSystem.Api.Dtos.Books;
using LibraryManagementSystem.Application.Services.Books;
using LibraryManagementSystem.Domain.Books.Entities;

namespace LibraryManagementSystem.Api.Routes;

public static class BooksEndpoint
{
    public static void MapBooksEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/books").WithTags("Books"); ;

        group.MapGet("/", async (IBookService bookService, int limit = 10, int page = 1) =>
        {
            return Results.Ok(await bookService.GetAll(limit, page));
        });

        group.MapGet("/{id:guid}", async (IBookService bookService, Guid id) =>
        {
            return Results.Ok(await bookService.GetById(id));
        });

        group.MapPost("/", async (IBookService bookService, CreateBookRequestDto requestDto) =>
        {
            return Results.Ok(await bookService.Create(new Book() { 
                Title = requestDto.Title,
                ISBN = requestDto.ISBN,
                PublishedDate = requestDto.PublishedDate,
                AuthorId = requestDto.AuthorId,
            }));
        });

        group.MapPut("/{id:guid}", async (IBookService bookService, Guid id, UpdateBookRequestDto requestDto) =>
        {
            return Results.Ok(await bookService.Update(id, new Book()
            {
                Title = requestDto.Title ?? string.Empty,
                ISBN = requestDto.ISBN ?? string.Empty,
                PublishedDate = requestDto.PublishedDate,
                AuthorId = requestDto.AuthorId,
            }));
        });

        group.MapDelete("/{id:guid}", async (IBookService bookService, Guid id) =>
        {
            await bookService.Delete(id);
            return Results.NoContent();
        });
    }
}
