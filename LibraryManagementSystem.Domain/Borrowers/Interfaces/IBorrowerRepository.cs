using LibraryManagementSystem.Domain.Borrowers.Entities;

namespace LibraryManagementSystem.Domain.Borrowers.Interfaces;
public interface IBorrowerRepository
{
    Task<List<Borrower>> GetAll(int limit, int page);
    Task<Borrower> GetById(Guid id);

    Task<Borrower> Add(Borrower borrower);

    Task<Borrower> Update(Guid id, Borrower borrower);

    Task DeleteById(Guid id);
}
