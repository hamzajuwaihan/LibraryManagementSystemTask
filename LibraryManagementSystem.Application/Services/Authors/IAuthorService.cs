using LibraryManagementSystem.Domain.Authors.Entities;

namespace LibraryManagementSystem.Application.Services.Authors;
public interface IAuthorService
{
    Task<Author> Create(Author author);
    Task<Author> Update(Guid id, Author author);
    Task<Author> GetById(Guid id);
    Task<List<Author>> GetAll(int limit, int page);
    Task Delete(Guid id);
}
