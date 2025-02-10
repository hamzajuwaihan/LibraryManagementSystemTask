using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using LibraryManagementSystem.Api.Dtos.Loans;
using LibraryManagementSystem.Application.Services.Loans;
using LibraryManagementSystem.Domain.Loans.Entities;

namespace LibraryManagementSystem.Api.Routes;

public static class LoansEndpoint
{
    public static void MapLoansEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/loans").WithTags("Loans").RequireAuthorization();

        group.MapPost("/", async (HttpContext httpContext, ILoanService loanService, CreateLoanRequestDto loanDto, IValidator<CreateLoanRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(loanDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }
            string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            Loan loan = new()
            {
                BookId = loanDto.BookId,
                BorrowerId = loanDto.BorrowerId,
                LoanDate = loanDto.LoanDate,
                CreatedBy = Guid.Parse(userId)
            };

            Loan createdLoan = await loanService.Create(loan);
            return Results.Created($"/api/loans/{createdLoan.Id}", createdLoan);
        });

        group.MapGet("/{id:guid}", async (ILoanService loanService, Guid id) => Results.Ok(await loanService.GetById(id)));

        group.MapGet("/", async (ILoanService loanservice, int limit = 10, int page = 1) => Results.Ok(await loanservice.GetAll(limit, page)));

        group.MapPut("/{id:guid}", async (HttpContext httpContext, ILoanService loanService, Guid id, UpdateLoanRequestDto loanDto, IValidator<UpdateLoanRequestDto> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(loanDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }
            string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            Loan loan = new()
            {
                ReturnDate = loanDto.ReturnDate,
                UpdatedBy = Guid.Parse(userId)
            };

            Loan updatedLoan = await loanService.Update(id, loan);
            return Results.Ok(updatedLoan);
        });
    }
}
