using FluentValidation;

namespace LibraryManagementSystem.Api.Dtos.Books.Validators;
public class BookRequestDtoValidator : AbstractValidator<BookRequestDto>
{
    public BookRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
            .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .MinimumLength(3).WithMessage("ISBN must be at least 3 characters long.")
            .MaximumLength(150).WithMessage("ISBN must not exceed 150 characters.");

        RuleFor(x => x.PublishedDate)
            .NotEmpty().WithMessage("Published date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Published date cannot be in the future.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("AuthorId is required.")
            .NotEqual(Guid.Empty).WithMessage("AuthorId cannot be an empty GUID.");
    }
}