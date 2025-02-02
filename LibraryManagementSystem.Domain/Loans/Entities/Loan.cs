using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Borrowers.Entities;
using LibraryManagementSystem.Domain.Shared.Entities;

namespace LibraryManagementSystem.Domain.Loans.Entities;

public class Loan : AuditableEntity
{
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Guid BookId { get; set; }
    public required Book Book { get; set; }
    public Guid BorrowerId { get; set; }
    public required Borrower Borrower { get; set; }
}
