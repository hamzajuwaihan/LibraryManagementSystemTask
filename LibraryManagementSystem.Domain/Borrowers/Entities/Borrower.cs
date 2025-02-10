using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Shared.Entities;

namespace LibraryManagementSystem.Domain.Borrowers.Entities;

/// <summary>
/// Represents a borrower in the library management system.
/// </summary>
public class Borrower : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the borrower.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the borrower.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the borrower.
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Gets or sets the list of loans associated with the borrower.
    /// </summary>
    public List<Loan> Loans { get; set; } = [];
}
