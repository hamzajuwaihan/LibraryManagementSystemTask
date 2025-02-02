using LibraryManagementSystem.Domain.Loans.Entities;

namespace LibraryManagementSystem.Application.Services.Loans;
public interface ILoanService
{
    Task<Loan> Create(Loan loan);
    Task<Loan> GetById(Guid id);
    Task<Loan> Update(Guid id, Loan loan);
}
