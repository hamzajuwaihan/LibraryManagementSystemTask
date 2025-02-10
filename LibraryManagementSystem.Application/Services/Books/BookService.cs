using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Authors.Interfaces;
using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Books.Interfaces;

namespace LibraryManagementSystem.Application.Services.Books;

/// <summary>
/// Provides book-related services such as creation, retrieval, and updates.
/// </summary>
/// <param name="bookRepository">The repository used for book data operations.</param>
/// <param name="authorRepository">The repository used for author data operations.</param>
public class BookService(IBookRepository bookRepository, IAuthorRepository authorRepository) : IBookService
{
    /// <summary>
    /// The repository used for author data operations.
    /// </summary>
    private readonly IAuthorRepository _authorRepository = authorRepository;

    /// <summary>
    /// The repository used for book data operations.
    /// </summary>
    private readonly IBookRepository _bookRepository = bookRepository;

    /// <summary>
    /// Creates a new book asynchronously, ensuring the associated author exists.
    /// </summary>
    /// <param name="book">The book entity to create.</param>
    /// <returns>The created <see cref="Book"/> entity.</returns>
    public async Task<Book> Create(Book book)
    {
        Author author = await _authorRepository.GetByIdAsync(book.AuthorId);

        book.Author = author;

        return await _bookRepository.AddAsync(book);
    }

    /// <summary>
    /// Deletes a book by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Delete(Guid id) => await _bookRepository.DeleteAsync(id);

    /// <summary>
    /// Retrieves a paginated list of books asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of books to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Book"/> entities for the requested page.</returns>
    public async Task<List<Book>> GetAll(int limit, int page) => await _bookRepository.GetAllAsync(limit, page);

    /// <summary>
    /// Retrieves a book by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book.</param>
    /// <returns>The <see cref="Book"/> entity if found.</returns>
    public async Task<Book> GetById(Guid id) => await _bookRepository.GetByIdAsync(id);

    /// <summary>
    /// Updates an existing book asynchronously, ensuring the associated author exists.
    /// </summary>
    /// <param name="id">The unique identifier of the book to update.</param>
    /// <param name="book">The updated book entity containing new values.</param>
    /// <returns>The updated <see cref="Book"/> entity.</returns>
    public async Task<Book> Update(Guid id, Book book)
    {
        Author author = await _authorRepository.GetByIdAsync(book.AuthorId);

        book.Author = author;

        return await _bookRepository.UpdateAsync(id, book);
    }
}
