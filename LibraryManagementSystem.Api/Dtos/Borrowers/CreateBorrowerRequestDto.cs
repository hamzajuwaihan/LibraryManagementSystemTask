namespace LibraryManagementSystem.Api.Dtos.Borrowers;

public class CreateBorrowerRequestDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
}
