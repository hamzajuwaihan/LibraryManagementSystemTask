using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Shared.Entities;

namespace LibraryManagementSystem.Domain.Borrowers.Entities;

public class Borrower : AuditableEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public List<Loan> Loans { get; set; } = [];

}
