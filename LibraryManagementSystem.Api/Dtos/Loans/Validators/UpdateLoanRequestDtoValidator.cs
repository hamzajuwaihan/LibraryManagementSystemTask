using FluentValidation;

namespace LibraryManagementSystem.Api.Dtos.Loans.Validators;

public class UpdateLoanRequestDtoValidator : AbstractValidator<UpdateLoanRequestDto>
{
    public UpdateLoanRequestDtoValidator()
    {
        RuleFor(x => x.ReturnDate)
            .NotEmpty().WithMessage("ReturnDate is required.");
    }

}
