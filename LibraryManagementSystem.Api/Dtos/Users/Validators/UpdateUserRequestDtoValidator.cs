using FluentValidation;

namespace LibraryManagementSystem.Api.Dtos.Users.Validators;

public class UpdateUserRequestDtoValidator : AbstractValidator<UpdateUserRequestDto>
{
    public UpdateUserRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must be provided")
            .EmailAddress().WithMessage("Email must be in a valid format");

        RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Password is required.")
        .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
        .MaximumLength(50).WithMessage("Password must not exceed 50 characters.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(5).WithMessage("Username must be at least 6 characters long.")
             .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");
    }
}
