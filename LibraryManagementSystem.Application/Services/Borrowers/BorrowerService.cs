using LibraryManagementSystem.Domain.Borrowers.Entities;
using LibraryManagementSystem.Domain.Borrowers.Interfaces;

namespace LibraryManagementSystem.Application.Services.Borrowers;

/// <summary>
/// Provides borrower-related services such as creation, retrieval, and updates.
/// </summary>
/// <param name="borrowerRepository">The repository used for borrower data operations.</param>
public class BorrowerService(IBorrowerRepository borrowerRepository) : IBorrowerService
{
    /// <summary>
    /// The repository used for borrower data operations.
    /// </summary>
    private readonly IBorrowerRepository _borrowerRepository = borrowerRepository;

    /// <summary>
    /// Creates a new borrower asynchronously.
    /// </summary>
    /// <param name="borrower">The borrower entity to create.</param>
    /// <returns>The created <see cref="Borrower"/> entity.</returns>
    public async Task<Borrower> Create(Borrower borrower) => await _borrowerRepository.AddAsync(borrower);

    /// <summary>
    /// Deletes a borrower by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the borrower to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Delete(Guid id) => await _borrowerRepository.DeleteById(id);

    /// <summary>
    /// Retrieves a paginated list of borrowers asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of borrowers to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Borrower"/> entities for the requested page.</returns>
    public async Task<List<Borrower>> GetAll(int limit, int page) => await _borrowerRepository.GetAllAsync(limit, page);

    /// <summary>
    /// Retrieves a borrower by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the borrower.</param>
    /// <returns>The <see cref="Borrower"/> entity if found.</returns>
    public async Task<Borrower> GetById(Guid id) => await _borrowerRepository.GetByIdAsync(id);

    /// <summary>
    /// Updates an existing borrower asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the borrower to update.</param>
    /// <param name="borrower">The updated borrower entity containing new values.</param>
    /// <returns>The updated <see cref="Borrower"/> entity.</returns>
    public async Task<Borrower> Update(Guid id, Borrower borrower) => await _borrowerRepository.UpdateAsync(id, borrower);
}
