using LibraryManagementSystem.Domain.Authors.Entities;

namespace LibraryManagementSystem.Application.Services.Authors;

/// <summary>
/// Defines the contract for author-related services.
/// </summary>
public interface IAuthorService
{
    /// <summary>
    /// Creates a new author asynchronously.
    /// </summary>
    /// <param name="author">The author entity to create.</param>
    /// <returns>The created <see cref="Author"/> entity.</returns>
    Task<Author> Create(Author author);

    /// <summary>
    /// Updates an existing author asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the author to update.</param>
    /// <param name="author">The updated author entity containing new values.</param>
    /// <returns>The updated <see cref="Author"/> entity.</returns>
    Task<Author> Update(Guid id, Author author);

    /// <summary>
    /// Retrieves an author by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the author.</param>
    /// <returns>The <see cref="Author"/> entity if found.</returns>
    Task<Author> GetById(Guid id);

    /// <summary>
    /// Retrieves a paginated list of authors asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of authors to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Author"/> entities for the requested page.</returns>
    Task<List<Author>> GetAll(int limit, int page);

    /// <summary>
    /// Deletes an author by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the author to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Delete(Guid id);
}
