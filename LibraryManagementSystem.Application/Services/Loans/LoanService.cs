using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Books.Interfaces;
using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Loans.Interfaces;
using LibraryManagementSystem.Domain.Shared.Exceptions;

namespace LibraryManagementSystem.Application.Services.Loans;

/// <summary>
/// Provides loan-related services such as creation, retrieval, and updates.
/// </summary>
/// <param name="loanRepository">The repository used for loan data operations.</param>
/// <param name="bookRepository">The repository used for book data operations.</param>
public class LoanService(ILoanRepository loanRepository, IBookRepository bookRepository) : ILoanService
{
    /// <summary>
    /// The repository used for loan data operations.
    /// </summary>
    private readonly ILoanRepository _loanRepository = loanRepository;

    /// <summary>
    /// The repository used for book data operations.
    /// </summary>
    private readonly IBookRepository _bookRepository = bookRepository;

    /// <summary>
    /// Creates a new loan asynchronously, ensuring the book is available and the loan date is valid.
    /// </summary>
    /// <param name="loan">The loan entity to create.</param>
    /// <returns>The created <see cref="Loan"/> entity.</returns>
    /// <exception cref="InvalidLoanDateException">Thrown when the loan date is in the future.</exception>
    /// <exception cref="BookAlreadyBorrowedException">Thrown when the book is already borrowed.</exception>
    public async Task<Loan> Create(Loan loan)
    {
        if (loan.LoanDate > DateTime.UtcNow)
            throw new InvalidLoanDateException("Loan date cannot be in the future.");

        Book book = await _bookRepository.GetByIdWithLoansAsync(loan.BookId);
        if (book.ActiveLoan != null)
            throw new BookAlreadyBorrowedException($"Book with ID {loan.BookId} is already borrowed.");

        return await _loanRepository.AddAsync(loan);
    }

    /// <summary>
    /// Retrieves a loan by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the loan.</param>
    /// <returns>The <see cref="Loan"/> entity if found.</returns>
    public async Task<Loan> GetById(Guid id) => await _loanRepository.GetByIdAsync(id);

    /// <summary>
    /// Updates an existing loan asynchronously, ensuring the return date is valid.
    /// </summary>
    /// <param name="id">The unique identifier of the loan to update.</param>
    /// <param name="loan">The updated loan entity containing new values.</param>
    /// <returns>The updated <see cref="Loan"/> entity.</returns>
    /// <exception cref="InvalidReturnDateException">Thrown when the return date is before the loan date.</exception>
    public async Task<Loan> Update(Guid id, Loan loan)
    {
        Loan existingLoan = await _loanRepository.GetByIdAsync(id);

        if (loan.ReturnDate.HasValue && loan.ReturnDate < existingLoan.LoanDate)
            throw new InvalidReturnDateException("Return date cannot be before the loan date.");

        return await _loanRepository.UpdateAsync(id, loan);
    }

    /// <summary>
    /// Retrieves a paginated list of loans asynchronously.
    /// </summary>
    /// <param name="limit">The maximum number of loans to retrieve per page.</param>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <returns>A list of <see cref="Loan"/> entities for the requested page.</returns>
    public async Task<List<Loan>> GetAll(int limit, int page) => await _loanRepository.GetAllAsync(limit, page);
}
