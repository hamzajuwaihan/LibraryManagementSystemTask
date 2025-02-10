using FluentValidation;

namespace LibraryManagementSystem.Api.Dtos.Users.Validators;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password must be provided");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be provided").EmailAddress().WithMessage("Email must be a valid format");
    }
}
