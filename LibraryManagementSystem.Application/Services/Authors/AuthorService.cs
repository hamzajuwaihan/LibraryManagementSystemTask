using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Authors.Interfaces;

namespace LibraryManagementSystem.Application.Services.Authors;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<Author> Create(Author author) => await _authorRepository.AddAsync(author);

    public async Task Delete(Guid id) => await _authorRepository.DeleteById(id);

    public async Task<List<Author>> GetAll(int limit, int page) => await _authorRepository.GetAllAsync(limit, page);

    public async Task<Author> GetById(Guid id) => await _authorRepository.GetByIdAsync(id);

    public async Task<Author> Update(Guid id, Author author) => await _authorRepository.UpdateAsync(id, author);
}
