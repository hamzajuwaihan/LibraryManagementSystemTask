namespace LibraryManagementSystem.Api.Dtos.Authors;

public class CreateAuthorRequestDto
{
    public required string Name { get; set; }

    public required string Bio { get; set; }
}
