using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using LibraryManagementSystem.Api.Dtos.Books;
using LibraryManagementSystem.Application.Services.Books;
using LibraryManagementSystem.Domain.Books.Entities;

namespace LibraryManagementSystem.Api.Routes;

public static class BooksEndpoint
{
    public static void MapBooksEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/books").WithTags("Books").RequireAuthorization();

        group.MapGet("/", async (IBookService bookService, int limit = 10, int page = 1) =>
        {
            return Results.Ok(await bookService.GetAll(limit, page));
        });

        group.MapGet("/{id:guid}", async (IBookService bookService, Guid id) =>
        {
            return Results.Ok(await bookService.GetById(id));
        });

        group.MapPost("/", async (HttpContext httpContext, IBookService bookService, BookRequestDto requestDto, IValidator<BookRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }
            string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            return Results.Ok(await bookService.Create(new Book()
            {
                Title = requestDto.Title,
                ISBN = requestDto.ISBN,
                PublishedDate = requestDto.PublishedDate,
                AuthorId = requestDto.AuthorId,
                CreatedBy = Guid.Parse(userId)
            }));
        }).RequireAuthorization("Admin");

        group.MapPut("/{id:guid}", async (HttpContext httpContext, IBookService bookService, Guid id, BookRequestDto requestDto, IValidator<BookRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }
            string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            return Results.Ok(await bookService.Update(id, new Book()
            {
                Title = requestDto.Title,
                ISBN = requestDto.ISBN,
                PublishedDate = requestDto.PublishedDate,
                AuthorId = requestDto.AuthorId,
                UpdatedBy = Guid.Parse(userId)
            }));
        });

        group.MapDelete("/{id:guid}", async (IBookService bookService, Guid id) =>
        {
            await bookService.Delete(id);
            return Results.NoContent();
        });
    }
}
