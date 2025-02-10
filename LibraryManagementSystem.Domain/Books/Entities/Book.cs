using System.Text.Json.Serialization;
using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Shared.Entities;

namespace LibraryManagementSystem.Domain.Books.Entities;

/// <summary>
/// Represents a book in the library system.
/// </summary>
public class Book : AuditableEntity
{
    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ISBN (International Standard Book Number) of the book.
    /// </summary>
    public string ISBN { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the book was published.
    /// </summary>
    public DateTime PublishedDate { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the author of the book.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the author associated with the book.
    /// </summary>
    public Author Author { get; set; }

    /// <summary>
    /// Gets or sets the list of loans associated with the book.
    /// This property is ignored during JSON serialization.
    /// </summary>
    [JsonIgnore]
    public List<Loan> Loans { get; set; } = [];

    /// <summary>
    /// Gets the active loan for the book, if any.
    /// Returns the first loan where the return date is not set.
    /// </summary>
    public Loan ActiveLoan => Loans.FirstOrDefault(l => l.ReturnDate == null);
}
