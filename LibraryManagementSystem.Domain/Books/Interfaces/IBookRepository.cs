using LibraryManagementSystem.Domain.Books.Entities;

namespace LibraryManagementSystem.Domain.Books.Interfaces;

public interface IBookRepository
{
    Task<Book> GetByIdAsync(Guid id);

    Task<List<Book>> GetAllAsync(int limit, int page);

    Task<Book> AddAsync(Book book);

    Task DeleteAsync(Guid id);

    Task<Book> Update(Guid id, Book book);
}
