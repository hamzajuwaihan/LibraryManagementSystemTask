using LibraryManagementSystem.Domain.Authors.Entities;
using LibraryManagementSystem.Domain.Books.Entities;
using LibraryManagementSystem.Domain.Borrowers.Entities;
using LibraryManagementSystem.Domain.Loans.Entities;
using LibraryManagementSystem.Domain.Users.Entities;
using LibraryManagementSystem.Domain.Users.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Database;

/// <summary>
/// Represents the database context for the Library Management System.
/// </summary>
/// <param name="options">The options to configure the database context.</param>
public class LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the Authors table.
    /// </summary>
    public DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Gets or sets the Books table.
    /// </summary>
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// Gets or sets the Loans table.
    /// </summary>
    public DbSet<Loan> Loans { get; set; }

    /// <summary>
    /// Gets or sets the Borrowers table.
    /// </summary>
    public DbSet<Borrower> Borrowers { get; set; }


    /// <summary>
    /// Gets or sets the Users table.
    /// </summary>
    public DbSet<User> Users { get; set; }


    /// <summary>
    /// Configures the entity relationships and database constraints.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure entity relationships.</param>
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

            entity.HasMany(b => b.Loans)
                  .WithOne(l => l.Book)
                  .HasForeignKey(l => l.BookId)
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
            entity.Property(u => u.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(u => u.Role).HasConversion<string>();

            entity.HasData(new User
            {
                Id = Guid.Parse("0194c896-3090-7299-a56e-31b8caabe9ef"),
                UserName = "hamza",
                Password = "AQAAAAIAAYagAAAAEDr5753O8U+quoxj18fC+cg5h0qcFKRFNvWAS+FewfiIxJo5mngACs0TOQk2R7Hfow==",
                Email = "hamza@gmail.com",
                Role = Role.Admin,
                CreatedAt = DateTime.SpecifyKind(
                    DateTime.Parse("2025-02-03 00:34:33.739714+03"),
                    DateTimeKind.Utc
                )
            });
        });
    }
}
