using System.Text.Json.Serialization;
using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Shared.Entities;

namespace LibraryManagementSystem.Domain.Authors.Entities;

/// <summary>
/// Represents an author in the library system.
/// </summary>
public class Author : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the author.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a brief biography of the author.
    /// </summary>
    public string Bio { get; set; }

    /// <summary>
    /// Gets or sets the list of books written by the author.
    /// This property is ignored during JSON serialization.
    /// </summary>
    [JsonIgnore]
    public List<Book> Books { get; set; } = [];
}
