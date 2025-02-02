using LibraryManagementSystem.Domain.Borrowers.Entities;
using LibraryManagementSystem.Domain.Borrowers.Interfaces;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;
public class BorrowerRepository(LibraryManagementDbContext context) : IBorrowerRepository
{
    private readonly LibraryManagementDbContext _context = context;

    public async Task<Borrower> Add(Borrower borrower)
    {
        await _context.Borrowers.AddAsync(borrower);
        await _context.SaveChangesAsync();
        return borrower;
    }

    public async Task DeleteById(Guid id)
    {
        Borrower borrower = await GetById(id);
        _context.Borrowers.Remove(borrower);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Borrower>> GetAll(int limit, int page)
    {
        return await _context.Borrowers
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }


    public async Task<Borrower> GetById(Guid id)
    {
        var borrower = await _context.Borrowers
            .Include(b => b.Loans)
            .FirstOrDefaultAsync(b => b.Id == id);

        return borrower ?? throw new ArgumentException("Borrower not found");
    }

    public async Task<Borrower> Update(Guid id, Borrower borrower)
    {
        var existingBorrower = await GetById(id);

        existingBorrower.Name = borrower.Name ?? existingBorrower.Name;
        existingBorrower.Email = borrower.Email ?? existingBorrower.Email;
        existingBorrower.Phone = borrower.Phone ?? existingBorrower.Phone;
        existingBorrower.UpdatedAt = DateTime.UtcNow;
        existingBorrower.UpdatedBy = borrower.UpdatedBy ?? existingBorrower.UpdatedBy;

        await _context.SaveChangesAsync();
        return existingBorrower;

    }
}
