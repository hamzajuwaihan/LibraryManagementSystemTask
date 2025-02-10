using LibraryManagementSystem.Application.Services.Jwt;
using LibraryManagementSystem.Domain.Shared.Exceptions;
using LibraryManagementSystem.Domain.Users.Entities;
using LibraryManagementSystem.Domain.Users.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Application.Services.Users;

/// <summary>
/// Provides user-related services such as authentication, creation, and updating.
/// </summary>
/// <param name="userRepository">The repository used for user data operations.</param>
/// <param name="jwtTokenService">The service for generating JWT tokens.</param>
public class UserService(IUserRepository userRepository, JwtTokenService jwtTokenService) : IUserService
{
    /// <summary>
    /// The repository used for user data operations.
    /// </summary>
    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// The password hasher for securely storing user passwords.
    /// </summary>
    private readonly PasswordHasher<User> _passwordHasher = new();

    /// <summary>
    /// The service used for generating JWT tokens.
    /// </summary>
    private readonly JwtTokenService _jwtTokenService = jwtTokenService;

    /// <summary>
    /// Creates a new user asynchronously, hashing their password before storing.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    /// <returns>The created <see cref="User"/> entity.</returns>
    public async Task<User> Create(User user)
    {
        user.Email = user.Email.ToLower();
        user.Password = _passwordHasher.HashPassword(user, user.Password);
        return await _userRepository.CreateAsync(user);
    }

    /// <summary>
    /// Retrieves a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The <see cref="User"/> entity if found.</returns>
    public async Task<User> GetById(Guid id) => await _userRepository.GetByIdAsync(id);

    /// <summary>
    /// Authenticates a user and generates a JWT token asynchronously.
    /// </summary>
    /// <param name="email">The email of the user attempting to log in.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>A JWT token if authentication is successful.</returns>
    /// <exception cref="InvalidCredentialsException">Thrown when the provided credentials are incorrect.</exception>
    public async Task<string> Login(string email, string password)
    {
        email = email.ToLower();

        User user = await _userRepository.GetByEmailAsync(email);

        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialsException();
        }

        return _jwtTokenService.GenerateJwtToken(user);
    }

    /// <summary>
    /// Updates an existing user asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="user">The updated user entity containing new values.</param>
    /// <returns>The updated <see cref="User"/> entity.</returns>
    public async Task<User> Update(Guid id, User user)
    {
        user.Email = user.Email.ToLower();
        return await _userRepository.UpdateAsync(id, user);
    }
}
