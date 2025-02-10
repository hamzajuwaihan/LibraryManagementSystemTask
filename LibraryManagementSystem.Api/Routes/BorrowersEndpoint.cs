using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using LibraryManagementSystem.Api.Dtos.Borrowers;
using LibraryManagementSystem.Application.Services.Borrowers;
using LibraryManagementSystem.Domain.Borrowers.Entities;

namespace LibraryManagementSystem.Api.Routes;

public static class BorrowersEndpoint
{
    public static void MapBorrowersEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/borrowers").WithTags("Borrowers").RequireAuthorization();

        group.MapPost("/", async (HttpContext httpContext, IBorrowerService borrowerService, BorrowerRequestDto borrower, IValidator<BorrowerRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(borrower);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }
            string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            Borrower createdBorrower = await borrowerService.Create(new Borrower() { Email = borrower.Email, Name = borrower.Name, Phone = borrower.Phone , CreatedBy = Guid.Parse(userId) });
            return Results.Created($"/api/borrowers/{createdBorrower.Id}", createdBorrower);
        });

        group.MapGet("/", async (IBorrowerService borrowerService, int limit = 10, int page = 1) => Results.Ok(await borrowerService.GetAll(limit, page)));

        group.MapGet("/{id:guid}", async (IBorrowerService borrowerService, Guid id) => Results.Ok(await borrowerService.GetById(id)));

        group.MapPut("/{id:guid}", async (HttpContext httpContext, IBorrowerService borrowerService, Guid id, BorrowerRequestDto borrower, IValidator<BorrowerRequestDto> validator) =>
        {

            ValidationResult validationResult = await validator.ValidateAsync(borrower);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }
            string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            return Results.Ok(await borrowerService.Update(id, new Borrower() { Email = borrower.Email, Name = borrower.Name, Phone = borrower.Phone, UpdatedBy = Guid.Parse(userId) }));
        });

        group.MapDelete("/{id:guid}", async (IBorrowerService borrowerService, Guid id) =>
        {
            await borrowerService.Delete(id);
            return Results.NoContent();
        });

    }
}
