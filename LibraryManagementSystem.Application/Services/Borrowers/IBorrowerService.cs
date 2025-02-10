using LibraryManagementSystem.Domain.Borrowers.Entities;

namespace LibraryManagementSystem.Application.Services.Borrowers;

/// <summary>
/// Defines the contract for borrower-related services.
/// </summary>
public interface IBorrowerService
{
    /// <summary>
    /// Creates a new borrower asynchronously.
    /// </summary>
    /// <param name="borrower">The borrower entity to create.</param>
    /// <returns>The created <see cref="Borrower"/> entity.</returns>
    Task<Borrower> Create(Borrower borrower);

    /// <summary>
    /// Updates an existing borrower asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the borrower to update.</param>
    /// <param name="borrower">The updated borrower entity containing new values.</param>
    /// <returns>The updated <see cref="Borrower"/> entity.</returns>
    Task<Borrower> Update(Guid id, Borrower borrower);

    /// <summary>
    /// Retrieves a borrower by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the borrower.</param>
    /// <returns>The <see cref="Borrower"/> entity if found.</returns>
    Task<Borrower> GetById(Guid id);

    /// <summary>
    /// Retrieves a paginated list of borrowers asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of borrowers to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Borrower"/> entities for the requested page.</returns>
    Task<List<Borrower>> GetAll(int limit, int page);

    /// <summary>
    /// Deletes a borrower by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the borrower to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Delete(Guid id);
}
