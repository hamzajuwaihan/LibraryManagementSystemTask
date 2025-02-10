using LibraryManagementSystem.Domain.Books.Entities;

namespace LibraryManagementSystem.Application.Services.Books;

/// <summary>
/// Defines the contract for book-related services.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Creates a new book asynchronously.
    /// </summary>
    /// <param name="book">The book entity to create.</param>
    /// <returns>The created <see cref="Book"/> entity.</returns>
    Task<Book> Create(Book book);

    /// <summary>
    /// Updates an existing book asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book to update.</param>
    /// <param name="book">The updated book entity containing new values.</param>
    /// <returns>The updated <see cref="Book"/> entity.</returns>
    Task<Book> Update(Guid id, Book book);

    /// <summary>
    /// Retrieves a book by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book.</param>
    /// <returns>The <see cref="Book"/> entity if found.</returns>
    Task<Book> GetById(Guid id);

    /// <summary>
    /// Retrieves a paginated list of books asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of books to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Book"/> entities for the requested page.</returns>
    Task<List<Book>> GetAll(int limit, int page);

    /// <summary>
    /// Deletes a book by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the book to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Delete(Guid id);
}
