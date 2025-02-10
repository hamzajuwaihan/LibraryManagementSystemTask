using LibraryManagementSystem.Domain.Borrowers.Entities;
using LibraryManagementSystem.Domain.Borrowers.Interfaces;
using LibraryManagementSystem.Domain.Shared.Exceptions;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;
/// <summary>
/// Provides repository operations for managing <see cref="Borrower"/> entities in the database.
/// </summary>
/// <param name="context">The database context for accessing borrower data.</param>
public class BorrowerRepository(LibraryManagementDbContext context) : IBorrowerRepository
{
    /// <summary>
    /// The database context for managing borrower entities.
    /// </summary>
    private readonly LibraryManagementDbContext _context = context;

    /// <summary>
    /// Adds a new borrower asynchronously.
    /// </summary>
    /// <param name="borrower">The borrower entity to add.</param>
    /// <returns>The created <see cref="Borrower"/> entity.</returns>
    /// <exception cref="EmailAlreadyUsedException">Thrown when the email is already in use by another borrower.</exception>
    public async Task<Borrower> AddAsync(Borrower borrower)
    {
        bool emailExists = await _context.Borrowers.AnyAsync(b => b.Email == borrower.Email);

        if (emailExists)
            throw new EmailAlreadyUsedException(borrower.Email);

        await _context.Borrowers.AddAsync(borrower);
        await _context.SaveChangesAsync();
        return borrower;
    }

    /// <summary>
    /// Deletes a borrower by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the borrower to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Thrown when no borrower with the specified ID is found.</exception>
    public async Task DeleteById(Guid id)
    {
        Borrower borrower = await GetByIdAsync(id);
        _context.Borrowers.Remove(borrower);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves a paginated list of borrowers asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of borrowers to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Borrower"/> entities for the requested page.</returns>
    public async Task<List<Borrower>> GetAllAsync(int limit, int page) => await _context.Borrowers
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

    /// <summary>
    /// Retrieves a borrower by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the borrower.</param>
    /// <returns>The <see cref="Borrower"/> entity if found.</returns>
    /// <exception cref="NotFoundException">Thrown when no borrower with the specified ID is found.</exception>
    public async Task<Borrower> GetByIdAsync(Guid id) => await _context.Borrowers
            .Include(b => b.Loans)
            .FirstOrDefaultAsync(b => b.Id == id) ?? throw new NotFoundException("Borrower", id);

    /// <summary>
    /// Updates an existing borrower asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the borrower to update.</param>
    /// <param name="borrower">The updated borrower entity containing new values.</param>
    /// <returns>The updated <see cref="Borrower"/> entity.</returns>
    /// <exception cref="NotFoundException">Thrown when the borrower to update does not exist.</exception>
    /// <exception cref="EmailAlreadyUsedException">Thrown when the updated email is already in use by another borrower.</exception>
    public async Task<Borrower> UpdateAsync(Guid id, Borrower borrower)
    {
        Borrower existingBorrower = await GetByIdAsync(id);

        if (borrower.Email != null && borrower.Email != existingBorrower.Email)
        {
            bool emailExists = await _context.Borrowers.AnyAsync(b => b.Email == borrower.Email && b.Id != id);

            if (emailExists)
                throw new EmailAlreadyUsedException(borrower.Email);
            existingBorrower.Email = borrower.Email;
        }

        existingBorrower.Name = borrower.Name ?? existingBorrower.Name;
        existingBorrower.Email = borrower.Email ?? existingBorrower.Email;
        existingBorrower.Phone = borrower.Phone ?? existingBorrower.Phone;
        existingBorrower.UpdatedAt = DateTime.UtcNow;
        existingBorrower.UpdatedBy = borrower.UpdatedBy ?? existingBorrower.UpdatedBy;

        await _context.SaveChangesAsync();
        return existingBorrower;
    }
}
