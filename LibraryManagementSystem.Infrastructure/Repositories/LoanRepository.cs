using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Loans.Interfaces;
using LibraryManagementSystem.Domain.Shared.Exceptions;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;
/// <summary>
/// Provides repository operations for managing <see cref="Loan"/> entities in the database.
/// </summary>
/// <param name="context">The database context for accessing loan data.</param>
public class LoanRepository(LibraryManagementDbContext context) : ILoanRepository
{
    /// <summary>
    /// The database context for managing loan entities.
    /// </summary>
    private readonly LibraryManagementDbContext _context = context;

    /// <summary>
    /// Adds a new loan asynchronously.
    /// </summary>
    /// <param name="loan">The loan entity to add.</param>
    /// <returns>The created <see cref="Loan"/> entity.</returns>
    public async Task<Loan> AddAsync(Loan loan)
    {
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return loan;
    }

    /// <summary>
    /// Retrieves a loan by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the loan.</param>
    /// <returns>The <see cref="Loan"/> entity if found.</returns>
    /// <exception cref="NotFoundException">Thrown when no loan with the specified ID is found.</exception>
    public async Task<Loan> GetByIdAsync(Guid id) => await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Borrower)
            .FirstOrDefaultAsync(l => l.Id == id) ?? throw new NotFoundException("Loan", id);

    /// <summary>
    /// Updates an existing loan asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the loan to update.</param>
    /// <param name="loan">The updated loan entity containing new values.</param>
    /// <returns>The updated <see cref="Loan"/> entity.</returns>
    /// <exception cref="NotFoundException">Thrown when the loan to update does not exist.</exception>
    public async Task<Loan> UpdateAsync(Guid id, Loan loan)
    {
        Loan existingLoan = await GetByIdAsync(id);

        existingLoan.LoanDate = loan.LoanDate != default ? loan.LoanDate : existingLoan.LoanDate;
        existingLoan.ReturnDate = loan.ReturnDate ?? existingLoan.ReturnDate;
        existingLoan.BookId = loan.BookId != Guid.Empty ? loan.BookId : existingLoan.BookId;
        existingLoan.BorrowerId = loan.BorrowerId != Guid.Empty ? loan.BorrowerId : existingLoan.BorrowerId;
        existingLoan.UpdatedAt = DateTime.UtcNow;
        existingLoan.UpdatedBy = loan.UpdatedBy ?? existingLoan.UpdatedBy;

        await _context.SaveChangesAsync();
        return existingLoan;

    }
    /// <summary>
    /// Retrieves a paginated list of loans asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of loans to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Loan"/> entities for the requested page.</returns>
    public async Task<List<Loan>> GetAllAsync(int limit, int page) => await _context.Loans.Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
}
