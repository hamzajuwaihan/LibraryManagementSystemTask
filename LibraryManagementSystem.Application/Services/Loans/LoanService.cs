using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Loans.Interfaces;

namespace LibraryManagementSystem.Application.Services.Loans;
public class LoanService(ILoanRepository loanRepository) : ILoanService
{
    private readonly ILoanRepository _loanRepository = loanRepository;
    public async Task<Loan> Create(Loan loan) => await _loanRepository.Add(loan);

    public async Task<Loan> GetById(Guid id) => await _loanRepository.GetById(id);

    public async Task<Loan> Update(Guid id, Loan loan) => await _loanRepository.Update(id, loan);
}
