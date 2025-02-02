using LibraryManagementSystem.Domain.Books.Entities;

namespace LibraryManagementSystem.Application.Services.Books;
public interface IBookService
{
    Task<Book> Create(Book book);

    Task<Book> Update(Guid id, Book book);

    Task<Book> GetById(Guid id);

    Task<List<Book>> GetAll(int limit, int page);

    Task Delete(Guid id);
}
