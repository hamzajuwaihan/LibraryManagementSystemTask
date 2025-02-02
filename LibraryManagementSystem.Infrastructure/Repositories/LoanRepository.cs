using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Loans.Interfaces;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;
public class LoanRepository(LibraryManagementDbContext context) : ILoanRepository
{
    private readonly LibraryManagementDbContext _context = context;

    public async Task<Loan> Add(Loan loan)
    {
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return loan;
    }


    public async Task<Loan> GetById(Guid id)
    {
        Loan? loan = await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Borrower)
            .FirstOrDefaultAsync(l => l.Id == id);

        return loan ?? throw new ArgumentException("Loan not found");
    }

    public async Task<Loan> Update(Guid id, Loan loan)
    {
        Loan existingLoan = await GetById(id);

        existingLoan.LoanDate = loan.LoanDate != default ? loan.LoanDate : existingLoan.LoanDate;
        existingLoan.ReturnDate = loan.ReturnDate ?? existingLoan.ReturnDate;
        existingLoan.BookId = loan.BookId != Guid.Empty ? loan.BookId : existingLoan.BookId;
        existingLoan.BorrowerId = loan.BorrowerId != Guid.Empty ? loan.BorrowerId : existingLoan.BorrowerId;
        existingLoan.UpdatedAt = DateTime.UtcNow;
        existingLoan.UpdatedBy = loan.UpdatedBy ?? existingLoan.UpdatedBy;

        await _context.SaveChangesAsync();
        return existingLoan;

    }
}
