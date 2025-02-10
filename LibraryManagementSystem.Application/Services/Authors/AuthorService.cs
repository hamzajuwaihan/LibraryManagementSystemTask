using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Authors.Interfaces;

namespace LibraryManagementSystem.Application.Services.Authors;

/// <summary>
/// Provides author-related services such as creation, retrieval, and updates.
/// </summary>
/// <param name="authorRepository">The repository used for author data operations.</param>
public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    /// <summary>
    /// The repository used for author data operations.
    /// </summary>
    private readonly IAuthorRepository _authorRepository = authorRepository;

    /// <summary>
    /// Creates a new author asynchronously.
    /// </summary>
    /// <param name="author">The author entity to create.</param>
    /// <returns>The created <see cref="Author"/> entity.</returns>
    public async Task<Author> Create(Author author) => await _authorRepository.AddAsync(author);

    /// <summary>
    /// Deletes an author by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the author to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Delete(Guid id) => await _authorRepository.DeleteById(id);

    /// <summary>
    /// Retrieves a paginated list of authors asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of authors to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Author"/> entities for the requested page.</returns>
    public async Task<List<Author>> GetAll(int limit, int page) => await _authorRepository.GetAllAsync(limit, page);

    /// <summary>
    /// Retrieves an author by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the author.</param>
    /// <returns>The <see cref="Author"/> entity if found.</returns>
    public async Task<Author> GetById(Guid id) => await _authorRepository.GetByIdAsync(id);

    /// <summary>
    /// Updates an existing author asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the author to update.</param>
    /// <param name="author">The updated author entity containing new values.</param>
    /// <returns>The updated <see cref="Author"/> entity.</returns>
    public async Task<Author> Update(Guid id, Author author) => await _authorRepository.UpdateAsync(id, author);
}
