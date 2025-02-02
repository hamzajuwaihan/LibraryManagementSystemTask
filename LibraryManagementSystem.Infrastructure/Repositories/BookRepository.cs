using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Books.Interfaces;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class BookRepository(LibraryManagementDbContext context) : IBookRepository
{
    private readonly LibraryManagementDbContext _context = context;

    public async Task<Book> AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task DeleteAsync(Guid id)
    {
        Book book = await GetByIdAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Book>> GetAllAsync(int limit, int page)
    {
        return await _context.Books
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        Book? book = await _context.Books.Include(b=> b.Author).FirstOrDefaultAsync(b => b.Id == id);
        return book ?? throw new ArgumentException("Book not found");
    }

    public async Task<Book> Update(Guid id, Book book)
    {
        var existingBook = await GetByIdAsync(id);
        if (existingBook != null)
        {
            existingBook.Title = book.Title ?? existingBook.Title;
            existingBook.ISBN = book.ISBN ?? existingBook.ISBN;
            existingBook.PublishedDate = book.PublishedDate != default ? book.PublishedDate : existingBook.PublishedDate;
            existingBook.AuthorId = book.AuthorId != Guid.Empty ? book.AuthorId : existingBook.AuthorId;
            existingBook.Author = book.Author ?? existingBook.Author;
            existingBook.CurrentLoan = book.CurrentLoan ?? existingBook.CurrentLoan;
            existingBook.UpdatedAt = DateTime.UtcNow;
            existingBook.UpdatedBy = book.UpdatedBy;

            await _context.SaveChangesAsync();
            return existingBook;
        }
        else
        {
            throw new ArgumentException("Book not found");
        }
    }

}
