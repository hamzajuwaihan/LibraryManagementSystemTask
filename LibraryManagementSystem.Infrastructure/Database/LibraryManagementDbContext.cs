using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Borrowers.Entities;
using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Database;

public class LibraryManagementDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; } 
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }

    public DbSet<User> Users { get; set; }
    public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options)
           : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Borrower>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Name).IsRequired().HasColumnType("VARCHAR(55)");
            entity.Property(b => b.Email).IsRequired().HasColumnType("VARCHAR(255)"); ;
            entity.Property(b => b.Phone).IsRequired().HasColumnType("VARCHAR(15)");
            entity.Property(b => b.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasMany(b => b.Loans)
                  .WithOne(l => l.Borrower)
                  .HasForeignKey(l => l.BorrowerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasColumnType("VARCHAR(55)"); ;
            entity.Property(a => a.Bio).IsRequired().HasColumnType("VARCHAR(255)"); ;
            entity.Property(b => b.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasMany(a => a.Books)
                  .WithOne(b => b.Author)
                  .HasForeignKey(b => b.AuthorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Title).IsRequired().HasColumnType("VARCHAR(50)"); ;
            entity.Property(b => b.ISBN).IsRequired().HasColumnType("VARCHAR(50)"); ;
            entity.Property(b => b.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(b => b.CurrentLoan)
                  .WithOne(l => l.Book)
                  .HasForeignKey<Loan>(l => l.BookId)
                  .OnDelete(DeleteBehavior.Restrict);
        });


        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.LoanDate).IsRequired();
            entity.Property(l => l.ReturnDate);
            entity.Property(b => b.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.Role).HasConversion<string>();
        });
    }
}
