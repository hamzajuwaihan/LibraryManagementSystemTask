using System.Text.Json.Serialization;
using LibraryManagementSystem.Domain.Users.Entities;
using LibraryManagementSystem.Domain.Users.Enums;

namespace LibraryManagementSystem.Api.Dtos.Users;

public class UserResponseDto(User user)
{
    public Guid Id { get; set; } = user.Id;
    public string? UserName { get; set; } = user.UserName;
    public string? Email { get; set; } = user.Email;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; } = user.Role;
    public DateTime CreatedAt { get; set; } = user.CreatedAt;
    public DateTime? UpdatedAt { get; set; } = user.UpdatedAt;
}