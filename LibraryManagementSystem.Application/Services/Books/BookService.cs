using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Books.Interfaces;

namespace LibraryManagementSystem.Application.Services.Books;
public class BookService(IBookRepository bookRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<Book> Create(Book book) => await _bookRepository.AddAsync(book);

    public async Task Delete(Guid id) => await _bookRepository.DeleteAsync(id);

    public async Task<List<Book>> GetAll(int limit, int page) => await _bookRepository.GetAllAsync(limit, page);

    public async Task<Book> GetById(Guid id) => await _bookRepository.GetByIdAsync(id);

    public async Task<Book> Update(Guid id, Book book) => await _bookRepository.Update(id, book);
}
