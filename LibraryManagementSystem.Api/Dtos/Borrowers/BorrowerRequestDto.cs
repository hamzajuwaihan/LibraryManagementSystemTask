namespace LibraryManagementSystem.Api.Dtos.Borrowers;

public class BorrowerRequestDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
}
