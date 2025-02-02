using LibraryManagementSystem.Domain.Users.Entities;

namespace LibraryManagementSystem.Domain.Users.Interfaces;
public interface IUserRepository
{
    Task<User> CreateAsync(User user);

    Task<User> GetById(Guid id);

    Task<User> UpdateAsync(Guid id, User user);
}
