using System.Text.Json.Serialization;
using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Borrowers.Entities;
using LibraryManagementSystem.Domain.Shared.Entities;

namespace LibraryManagementSystem.Domain.Loans.Entities;

/// <summary>
/// Represents a loan transaction in the library system.
/// </summary>
public class Loan : AuditableEntity
{
    /// <summary>
    /// Gets or sets the date when the loan was issued.
    /// </summary>
    public DateTime LoanDate { get; set; }

    /// <summary>
    /// Gets or sets the date when the book was returned, if applicable.
    /// </summary>
    public DateTime? ReturnDate { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the borrowed book.
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Gets or sets the book associated with the loan.
    /// This property is ignored during JSON serialization.
    /// </summary>
    [JsonIgnore]
    public Book Book { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the borrower.
    /// </summary>
    public Guid BorrowerId { get; set; }

    /// <summary>
    /// Gets or sets the borrower associated with the loan.
    /// This property is ignored during JSON serialization.
    /// </summary>
    [JsonIgnore]
    public Borrower Borrower { get; set; }
}
