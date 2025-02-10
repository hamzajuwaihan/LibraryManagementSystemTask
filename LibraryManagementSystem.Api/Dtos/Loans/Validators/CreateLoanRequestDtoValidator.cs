using FluentValidation;

namespace LibraryManagementSystem.Api.Dtos.Loans.Validators;

public class CreateLoanRequestDtoValidator : AbstractValidator<CreateLoanRequestDto>
{
    public CreateLoanRequestDtoValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("BookId is required.")
            .NotEqual(Guid.Empty).WithMessage("BookId cannot be an empty GUID.");

        RuleFor(x => x.BorrowerId)
            .NotEmpty().WithMessage("BorrowerId is required.")
            .NotEqual(Guid.Empty).WithMessage("BorrowerId cannot be an empty GUID.");

        RuleFor(x => x.LoanDate)
            .NotEmpty().WithMessage("LoanDate is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("LoanDate cannot be in the future.");
    }
}
