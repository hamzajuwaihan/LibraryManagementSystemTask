using LibraryManagementSystem.Domain.Borrowers.Entities;
using LibraryManagementSystem.Domain.Borrowers.Interfaces;

namespace LibraryManagementSystem.Application.Services.Borrowers;
public class BorrowerService(IBorrowerRepository borrowerRepository) : IBorrowerService
{
    private readonly IBorrowerRepository _borrowerRepository = borrowerRepository;
    public async Task<Borrower> Create(Borrower borrower) => await _borrowerRepository.Add(borrower);

    public async Task Delete(Guid id) => await _borrowerRepository.DeleteById(id);

    public async Task<List<Borrower>> GetAll(int limit, int page) => await _borrowerRepository.GetAll(limit, page);

    public async Task<Borrower> GetById(Guid id) => await _borrowerRepository.GetById(id);

    public async Task<Borrower> Update(Guid id, Borrower borrower) => await _borrowerRepository.Update(id, borrower);
}
