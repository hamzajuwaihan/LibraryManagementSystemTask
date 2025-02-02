using LibraryManagementSystem.Domain.Loans.Entities;

namespace LibraryManagementSystem.Domain.Loans.Interfaces;
public interface ILoanRepository
{
    Task<Loan> Add(Loan loan);
    Task<Loan> GetById(Guid id);
    Task<Loan> Update(Guid id, Loan loan);
}
