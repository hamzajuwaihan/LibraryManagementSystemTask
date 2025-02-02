using LibraryManagementSystem.Domain.Borrowers.Entities;

namespace LibraryManagementSystem.Application.Services.Borrowers;
public interface IBorrowerService
{
    Task<Borrower> Create(Borrower borrower);

    Task<Borrower> Update(Guid id, Borrower borrower);

    Task<Borrower> GetById(Guid id);

    Task<List<Borrower>> GetAll(int limit, int page);

    Task Delete(Guid id);
}
