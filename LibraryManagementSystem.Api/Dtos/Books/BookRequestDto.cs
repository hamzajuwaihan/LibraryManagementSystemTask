namespace LibraryManagementSystem.Api.Dtos.Books;

public class BookRequestDto
{
    public string? Title { get; set; }
    public string? ISBN { get; set; }
    public DateTime PublishedDate { get; set; }
    public Guid AuthorId { get; set; }
}
