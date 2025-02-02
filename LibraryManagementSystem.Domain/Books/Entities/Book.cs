using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Shared.Entities;

namespace LibraryManagementSystem.Domain.Books.Entities;

public class Book : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public Guid AuthorId { get; set; }
    public Author? Author { get; set; }
    public Loan? CurrentLoan { get; set; }
}
