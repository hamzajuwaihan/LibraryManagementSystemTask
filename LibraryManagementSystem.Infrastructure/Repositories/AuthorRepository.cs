using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Authors.Interfaces;
using LibraryManagementSystem.Domain.Shared.Exceptions;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

/// <summary>
/// Provides repository operations for managing <see cref="Author"/> entities in the database.
/// </summary>
/// <param name="context">The database context for accessing author data.</param>
public class AuthorRepository(LibraryManagementDbContext context) : IAuthorRepository
{
    /// <summary>
    /// The database context for managing author entities.
    /// </summary>
    private readonly LibraryManagementDbContext _context = context;

    /// <summary>
    /// Adds a new author asynchronously.
    /// </summary>
    /// <param name="author">The author entity to add.</param>
    /// <returns>The created <see cref="Author"/> entity.</returns>
    public async Task<Author> AddAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    /// <summary>
    /// Deletes an author by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the author to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Thrown when no author with the specified ID is found.</exception>
    public async Task DeleteById(Guid id)
    {
        Author author = await GetByIdAsync(id);
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

    }

    /// <summary>
    /// Retrieves a paginated list of authors asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of authors to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Author"/> entities for the requested page.</returns>
    public async Task<List<Author>> GetAllAsync(int limit, int page) => await _context.Authors
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

    /// <summary>
    /// Retrieves an author by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the author.</param>
    /// <returns>The <see cref="Author"/> entity if found.</returns>
    /// <exception cref="NotFoundException">Thrown when no author with the specified ID is found.</exception>
    public async Task<Author> GetByIdAsync(Guid id) => await _context.Authors.FindAsync(id) ?? throw new NotFoundException("Author", id);

    /// <summary>
    /// Updates an existing author asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the author to update.</param>
    /// <param name="author">The updated author entity containing new values.</param>
    /// <returns>The updated <see cref="Author"/> entity.</returns>
    /// <exception cref="NotFoundException">Thrown when the author to update does not exist.</exception>
    public async Task<Author> UpdateAsync(Guid id, Author author)
    {
        Author existingAuthor = await GetByIdAsync(id); 

        existingAuthor.Name = author.Name ?? existingAuthor.Name;
        existingAuthor.Bio = author.Bio ?? existingAuthor.Bio;
        existingAuthor.UpdatedAt = DateTime.UtcNow;
        existingAuthor.UpdatedBy = author.UpdatedBy ?? existingAuthor.UpdatedBy;

        await _context.SaveChangesAsync();
        return existingAuthor;
    }
}
