using LibraryManagementSystem.Domain.Users.Entities;

namespace LibraryManagementSystem.Domain.Users.Interfaces;

/// <summary>
/// Defines the contract for user repository operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Creates a new user asynchronously.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    /// <returns>The created <see cref="User"/> entity.</returns>
    Task<User> CreateAsync(User user);

    /// <summary>
    /// Retrieves a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The <see cref="User"/> entity if found.</returns>
    Task<User> GetByIdAsync(Guid id);

    /// <summary>
    /// Updates an existing user asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="user">The updated user entity containing new values.</param>
    /// <returns>The updated <see cref="User"/> entity.</returns>
    Task<User> UpdateAsync(Guid id, User user);

    /// <summary>
    /// Retrieves a user by their email address asynchronously.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>The <see cref="User"/> entity if found.</returns>
    Task<User> GetByEmailAsync(string email);
}
