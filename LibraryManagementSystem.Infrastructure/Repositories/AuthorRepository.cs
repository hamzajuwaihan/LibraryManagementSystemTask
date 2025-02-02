using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Authors.Interfaces;
using LibraryManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;
public class AuthorRepository(LibraryManagementDbContext context) : IAuthorRepository
{
    private readonly LibraryManagementDbContext _context = context;

    public async Task<Author> AddAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task DeleteById(Guid id)
    {
        Author author = await GetByIdAsync(id);
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

    }

    public async Task<List<Author>> GetAllAsync(int limit, int page)
    {
        return await _context.Authors
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<Author> GetByIdAsync(Guid id)
    {
        Author? author = await _context.Authors.FindAsync(id);
        return author ?? throw new ArgumentException("Author not found");
    }

    public async Task<Author> UpdateAsync(Guid id, Author author)
    {
        Author existingAuthor = await GetByIdAsync(id); 

        existingAuthor.Name = author.Name ?? existingAuthor.Name;
        existingAuthor.Bio = author.Bio ?? existingAuthor.Bio;
        existingAuthor.UpdatedAt = DateTime.UtcNow;
        existingAuthor.UpdatedBy = author.UpdatedBy ?? existingAuthor.UpdatedBy;

        await _context.SaveChangesAsync();
        return existingAuthor;
    }
}
