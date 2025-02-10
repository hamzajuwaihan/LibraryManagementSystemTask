using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LibraryManagementSystem.Domain.Users.Enums;

namespace LibraryManagementSystem.Api.Dtos.Users;

public class UpdateUserRequestDto
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    [DefaultValue(Role.User)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; } = Role.User;
}
