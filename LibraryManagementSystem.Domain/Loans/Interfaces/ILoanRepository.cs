using LibraryManagementSystem.Domain.Loans.Entities;

namespace LibraryManagementSystem.Domain.Loans.Interfaces;

/// <summary>
/// Defines the contract for loan repository operations.
/// </summary>
public interface ILoanRepository
{
    /// <summary>
    /// Adds a new loan asynchronously.
    /// </summary>
    /// <param name="loan">The loan entity to add.</param>
    /// <returns>The created <see cref="Loan"/> entity.</returns>
    Task<Loan> AddAsync(Loan loan);

    /// <summary>
    /// Retrieves a loan by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the loan.</param>
    /// <returns>The <see cref="Loan"/> entity if found.</returns>
    Task<Loan> GetByIdAsync(Guid id);


    /// <summary>
    /// Updates an existing loan asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the loan to update.</param>
    /// <param name="loan">The updated loan entity containing new values.</param>
    /// <returns>The updated <see cref="Loan"/> entity.</returns>
    Task<Loan> UpdateAsync(Guid id, Loan loan);

    /// <summary>
    /// Retrieves a paginated list of loans asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of loans to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Loan"/> entities for the requested page.</returns
    Task<List<Loan>> GetAllAsync(int limit, int page);
}
