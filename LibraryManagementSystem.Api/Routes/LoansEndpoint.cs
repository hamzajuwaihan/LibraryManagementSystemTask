using LibraryManagementSystem.Application.Services.Loans;
using LibraryManagementSystem.Domain.Loans.Entities;

namespace LibraryManagementSystem.Api.Routes;

public static class LoansEndpoint
{
    public static void MapLoansEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/loans").WithTags("Loans");

        group.MapPost("/", async (ILoanService loanService, Loan loan) =>
        {
            var createdLoan = await loanService.Create(loan);
            return Results.Created($"/api/loans/{createdLoan.Id}", createdLoan);
        });

        group.MapGet("/{id:guid}", async (ILoanService loanService, Guid id) => Results.Ok(await loanService.GetById(id)));

        group.MapPut("/{id:guid}", async (ILoanService loanService, Guid id, Loan loan) =>
        {
            var updatedLoan = await loanService.Update(id, loan);
            return Results.Ok(updatedLoan);
        });
    }
}
