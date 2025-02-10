using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Authors.Interfaces;
using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Books.Interfaces;
using LibraryManagementSystem.Domain.Shared.Exceptions;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

/// <summary>
/// Provides repository operations for managing <see cref="Book"/> entities in the database.
/// </summary>
/// <param name="context">The database context for accessing book data.</param>
/// <param name="authorRepository">The repository for managing author entities.</param>
public class BookRepository(LibraryManagementDbContext context, IAuthorRepository authorRepository) : IBookRepository
{
    /// <summary>
    /// The database context for managing book entities.
    /// </summary>
    private readonly LibraryManagementDbContext _context = context;

    /// <summary>
    /// The repository for managing author entities.
    /// </summary>
    private readonly IAuthorRepository _authorRepository = authorRepository;

    /// <summary>
    /// Adds a new book asynchronously.
    /// </summary>
    /// <param name="book">The book entity to add.</param>
    /// <returns>The created <see cref="Book"/> entity.</returns>
    public async Task<Book> AddAsync(Book book)
    {
        Author author = await _authorRepository.GetByIdAsync(book.AuthorId);
        book.Author = author;
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    /// <summary>
    /// Deletes a book by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Thrown when no book with the specified ID is found.</exception>
    public async Task DeleteAsync(Guid id)
    {
        Book book = await GetByIdAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Retrieves a paginated list of books asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of books to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Book"/> entities for the requested page.</returns>
    public async Task<List<Book>> GetAllAsync(int limit, int page) => await _context.Books
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

    /// <summary>
    /// Retrieves a book by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book.</param>
    /// <returns>The <see cref="Book"/> entity if found.</returns>
    /// <exception cref="NotFoundException">Thrown when no book with the specified ID is found.</exception>
    public async Task<Book> GetByIdAsync(Guid id) => 
        await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id) ?? throw new NotFoundException("Book", id);

    /// <summary>
    /// Updates an existing book asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book to update.</param>
    /// <param name="book">The updated book entity containing new values.</param>
    /// <returns>The updated <see cref="Book"/> entity.</returns>
    /// <exception cref="NotFoundException">Thrown when the book to update does not exist.</exception>
    public async Task<Book> UpdateAsync(Guid id, Book book)
    {
        Book existingBook = await GetByIdAsync(id);

            existingBook.Title = book.Title ?? existingBook.Title;
            existingBook.ISBN = book.ISBN ?? existingBook.ISBN;
            existingBook.PublishedDate = book.PublishedDate != default ? book.PublishedDate : existingBook.PublishedDate;
            existingBook.AuthorId = book.AuthorId != Guid.Empty ? book.AuthorId : existingBook.AuthorId;
            existingBook.Author = book.Author ?? existingBook.Author;
            existingBook.UpdatedAt = DateTime.UtcNow;
            existingBook.UpdatedBy = book.UpdatedBy;

            await _context.SaveChangesAsync();
            return existingBook;
    }

    /// <summary>
    /// Retrieves a book by its unique identifier, including loan information asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book.</param>
    /// <returns>The <see cref="Book"/> entity if found.</returns>
    /// <exception cref="NotFoundException">Thrown when no book with the specified ID is found.</exception>
    public async Task<Book> GetByIdWithLoansAsync(Guid id) =>
    await _context.Books
        .Include(b => b.Loans)
        .FirstOrDefaultAsync(b => b.Id == id)
    ?? throw new NotFoundException("Book", id);
}
