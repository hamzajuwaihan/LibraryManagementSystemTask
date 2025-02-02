using LibraryManagementSystem.Domain.Shared.Entities;
using LibraryManagementSystem.Domain.Users.Enums;

namespace LibraryManagementSystem.Domain.Users.Entities;

public class User : BaseEntity
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public Role Role { get; set; } = Role.User;
}
