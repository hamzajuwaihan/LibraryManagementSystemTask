using LibraryManagementSystem.Domain.Users.Entities;
using LibraryManagementSystem.Domain.Users.Interfaces;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;
public class UserRepository(LibraryManagementDbContext context) : IUserRepository
{
    private readonly LibraryManagementDbContext _context = context;

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetById(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        return user ?? throw new ArgumentException("User not found");
    }

    public async Task<User> UpdateAsync(Guid id, User user)
    {
        var existingUser = await GetById(id);

        existingUser.UserName = user.UserName ?? existingUser.UserName;
        existingUser.Password = user.Password ?? existingUser.Password;
        existingUser.Email = user.Email ?? existingUser.Email;

        await _context.SaveChangesAsync();
        return existingUser;
    }
}
