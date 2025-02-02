using System.Text.Json.Serialization;
using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Shared.Entities;

namespace LibraryManagementSystem.Domain.Authors.Entities;

public class Author : AuditableEntity
{
    public required string Name { get; set; }
    public required string Bio { get; set; }
    [JsonIgnore]
    public List<Book> Books { get; set; } = [];
}
