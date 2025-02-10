using FluentValidation;

namespace LibraryManagementSystem.Api.Dtos.Authors.Validators;

public class AuthorRequestDtoValidator : AbstractValidator<AuthorRequestDto>
{
    public AuthorRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(x => x.Bio)
            .NotEmpty().WithMessage("Bio is required.")
            .MinimumLength(10).WithMessage("Bio must be at least 10 characters long.")
            .MaximumLength(50).WithMessage("Bio must not exceed 50 characters.");
    }
}
