using LibraryManagementSystem.Domain.Shared.Entities;
using LibraryManagementSystem.Domain.Users.Enums;

namespace LibraryManagementSystem.Domain.Users.Entities;

/// <summary>
/// Represents a user in the library management system.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hashed password of the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public Role Role { get; set; } = Role.User;
}
