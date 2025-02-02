using LibraryManagementSystem.Domain.Authors.Entities;

namespace LibraryManagementSystem.Domain.Authors.Interfaces;

public interface IAuthorRepository
{
    Task<Author> AddAsync(Author author);

    Task<Author> UpdateAsync(Guid id, Author author);

    Task<Author> GetByIdAsync(Guid id);

    Task<List<Author>> GetAllAsync(int limit, int page);

    Task DeleteById(Guid id);
}
