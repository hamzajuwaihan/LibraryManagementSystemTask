using LibraryManagementSystem.Api.Dtos.Borrowers;
using LibraryManagementSystem.Application.Services.Borrowers;
using LibraryManagementSystem.Domain.Borrowers.Entities;

namespace LibraryManagementSystem.Api.Routes;

public static class BorrowersEndpoint
{
    public static void MapBorrowersEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/borrowers").WithTags("Borrowers");

        group.MapPost("/", async (IBorrowerService borrowerService, CreateBorrowerRequestDto borrower) =>
        {
            var createdBorrower = await borrowerService.Create(new Borrower() { Email= borrower.Email, Name = borrower.Name, Phone = borrower.Phone});
            return Results.Created($"/api/borrowers/{createdBorrower.Id}", createdBorrower);
        });

        group.MapGet("/", async (IBorrowerService borrowerService, int limit = 10, int page = 1) => Results.Ok(await borrowerService.GetAll(limit, page)));

        group.MapGet("/{id:guid}", async (IBorrowerService borrowerService, Guid id) => Results.Ok(await borrowerService.GetById(id)));

        group.MapPut("/{id:guid}", async (IBorrowerService borrowerService, Guid id, UpdateBorrowerRequestDto borrower) => 
                        Results.Ok(await borrowerService.Update(id, new Borrower() { Email = borrower.Email, Name = borrower.Name, Phone = borrower.Phone })));

        group.MapDelete("/{id:guid}", async (IBorrowerService borrowerService, Guid id) =>
        {
            await borrowerService.Delete(id);
            return Results.NoContent();
        });

    }
}
