using LibraryManagementSystem.Domain.Shared.Exceptions;
using LibraryManagementSystem.Domain.Users.Entities;
using LibraryManagementSystem.Domain.Users.Interfaces;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;
/// <summary>
/// Provides repository operations for managing <see cref="User"/> entities in the database.
/// </summary>
/// <param name="context">The database context for accessing user data.</param>
public class UserRepository(LibraryManagementDbContext context) : IUserRepository
{
    /// <summary>
    /// The database context for managing user entities.
    /// </summary>
    private readonly LibraryManagementDbContext _context = context;

    /// <summary>
    /// Creates a new user asynchronously.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    /// <returns>The created <see cref="User"/> entity.</returns>
    /// <exception cref="EmailAlreadyUsedException">Thrown when the email is already in use by another user.</exception>
    public async Task<User> CreateAsync(User user)
    {
        bool emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email);
        if (emailExists)
            throw new EmailAlreadyUsedException(user.Email);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    /// <summary>
    /// Retrieves a user by their email address asynchronously.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>The <see cref="User"/> entity if found.</returns>
    /// <exception cref="NotFoundException">Thrown when no user with the specified email is found.</exception>
    public async Task<User> GetByEmailAsync(string email) =>
                await _context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new NotFoundException("User", email);

    /// <summary>
    /// Retrieves a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The <see cref="User"/> entity if found.</returns>
    /// <exception cref="NotFoundException">Thrown when no user with the specified ID is found.</exception>
    public async Task<User> GetByIdAsync(Guid id) =>
                            await _context.Users.FindAsync(id) ?? throw new NotFoundException("User", id);

    /// <summary>
    /// Updates an existing user asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="user">The updated user entity containing new values.</param>
    /// <returns>The updated <see cref="User"/> entity.</returns>
    /// <exception cref="NotFoundException">Thrown when the user to update does not exist.</exception>
    /// <exception cref="EmailAlreadyUsedException">Thrown when the updated email is already in use by another user.</exception>
    public async Task<User> UpdateAsync(Guid id, User user)
    {
        User existingUser = await GetByIdAsync(id);

        if (user.Email != null && user.Email != existingUser.Email)
        {
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email && u.Id != id);
            if (emailExists)
                throw new EmailAlreadyUsedException(user.Email);
            existingUser.Email = user.Email;
        }

        existingUser.UserName = user.UserName ?? existingUser.UserName;
        existingUser.Password = user.Password ?? existingUser.Password;
        existingUser.Email = user.Email ?? existingUser.Email;

        await _context.SaveChangesAsync();
        return existingUser;
    }
}
