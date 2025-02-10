using LibraryManagementSystem.Domain.Books.Entities;

namespace LibraryManagementSystem.Domain.Books.Interfaces;

/// <summary>
/// Defines the contract for book repository operations.
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Retrieves a book by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book.</param>
    /// <returns>The <see cref="Book"/> entity if found.</returns>
    Task<Book> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves a paginated list of books asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of books to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Book"/> entities for the requested page.</returns>
    Task<List<Book>> GetAllAsync(int limit, int page);

    /// <summary>
    /// Adds a new book asynchronously.
    /// </summary>
    /// <param name="book">The book entity to add.</param>
    /// <returns>The created <see cref="Book"/> entity.</returns>
    Task<Book> AddAsync(Book book);

    /// <summary>
    /// Deletes a book by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Updates an existing book asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book to update.</param>
    /// <param name="book">The updated book entity containing new values.</param>
    /// <returns>The updated <see cref="Book"/> entity.</returns>
    Task<Book> UpdateAsync(Guid id, Book book);

    /// <summary>
    /// Retrieves a book by its unique identifier, including loan information asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book.</param>
    /// <returns>The <see cref="Book"/> entity if found.</returns>
    Task<Book> GetByIdWithLoansAsync(Guid id);
}
