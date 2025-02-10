using LibraryManagementSystem.Domain.Users.Entities;

namespace LibraryManagementSystem.Application.Services.Users;

/// <summary>
/// Defines the contract for user-related services.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a new user asynchronously.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    /// <returns>The created <see cref="User"/> entity.</returns>
    Task<User> Create(User user);

    /// <summary>
    /// Retrieves a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The <see cref="User"/> entity if found.</returns>
    Task<User> GetById(Guid id);

    /// <summary>
    /// Updates an existing user asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="user">The updated user entity containing new values.</param>
    /// <returns>The updated <see cref="User"/> entity.</returns>
    Task<User> Update(Guid id, User user);

    /// <summary>
    /// Authenticates a user and generates a JWT token asynchronously.
    /// </summary>
    /// <param name="email">The email of the user attempting to log in.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>A JWT token if authentication is successful.</returns>
    /// <exception cref="InvalidCredentialsException">Thrown when the provided credentials are incorrect.</exception>
    Task<string> Login(string email, string password);
}
