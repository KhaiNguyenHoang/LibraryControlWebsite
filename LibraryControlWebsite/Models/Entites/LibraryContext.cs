using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace LibaryControlWebsite.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Bookcopy> Bookcopies { get; set; }

    public virtual DbSet<Bookreview> Bookreviews { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Fine> Fines { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Waitlist> Waitlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=LibraryManagement;user=root;password=admin", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PRIMARY");

            entity.ToTable("authors");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Biography)
                .HasColumnType("text")
                .HasColumnName("biography");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("books");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.HasIndex(e => e.Isbn, "isbn").IsUnique();

            entity.HasIndex(e => e.Title, "title").HasAnnotation("MySql:FullTextIndex", true);

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.AvailableCopies)
                .HasDefaultValueSql("'1'")
                .HasColumnName("available_copies");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.PublishYear)
                .HasColumnType("year")
                .HasColumnName("publish_year");
            entity.Property(e => e.Publisher)
                .HasMaxLength(100)
                .HasColumnName("publisher");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TotalCopies)
                .HasDefaultValueSql("'1'")
                .HasColumnName("total_copies");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("books_ibfk_1");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "Bookauthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("bookauthors_ibfk_2"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("bookauthors_ibfk_1"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("bookauthors");
                        j.HasIndex(new[] { "AuthorId" }, "author_id");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("author_id");
                    });
        });

        modelBuilder.Entity<Bookcopy>(entity =>
        {
            entity.HasKey(e => e.CopyId).HasName("PRIMARY");

            entity.ToTable("bookcopies");

            entity.HasIndex(e => e.BookId, "book_id");

            entity.Property(e => e.CopyId).HasColumnName("copy_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Conditions)
                .HasDefaultValueSql("'Good'")
                .HasColumnType("enum('New','Good','Damaged')")
                .HasColumnName("conditions");
            entity.Property(e => e.CopyNumber).HasColumnName("copy_number");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_available");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");

            entity.HasOne(d => d.Book).WithMany(p => p.Bookcopies)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("bookcopies_ibfk_1");
        });

        modelBuilder.Entity<Bookreview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PRIMARY");

            entity.ToTable("bookreviews");

            entity.HasIndex(e => e.BookId, "book_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Bookreviews)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("bookreviews_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Bookreviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("bookreviews_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.HasIndex(e => e.CategoryName, "category_name").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
        });

        modelBuilder.Entity<Fine>(entity =>
        {
            entity.HasKey(e => e.FineId).HasName("PRIMARY");

            entity.ToTable("fines");

            entity.HasIndex(e => e.LoanId, "loan_id");

            entity.Property(e => e.FineId).HasColumnName("fine_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.Paid)
                .HasDefaultValueSql("'0'")
                .HasColumnName("paid");
            entity.Property(e => e.PaidDate).HasColumnName("paid_date");

            entity.HasOne(d => d.Loan).WithMany(p => p.Fines)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("fines_ibfk_1");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PRIMARY");

            entity.ToTable("loans");

            entity.HasIndex(e => e.BookId, "book_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.LoanDate)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("loan_date");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Borrowed'")
                .HasColumnType("enum('Borrowed','Returned','Overdue')")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Loans)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("loans_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Loans)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("loans_ibfk_1");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.PermissionName, "permission_name").IsUnique();

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .HasColumnName("permission_name");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PRIMARY");

            entity.ToTable("reservations");

            entity.HasIndex(e => e.BookId, "book_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.ReservationId).HasColumnName("reservation_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.ReservationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("reservation_date");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Pending'")
                .HasColumnType("enum('Pending','Fulfilled','Cancelled')")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("reservations_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("reservations_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "role_name").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "Rolepermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("rolepermissions_ibfk_2"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("rolepermissions_ibfk_1"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("rolepermissions");
                        j.HasIndex(new[] { "PermissionId" }, "permission_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Phone, "phone").IsUnique();

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasComment("Bcrypt hashed password")
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("role_id");
            entity.Property(e => e.UserType)
                .HasDefaultValueSql("'Reader'")
                .HasColumnType("enum('Reader','Staff','Admin')")
                .HasColumnName("user_type");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_ibfk_1");

            entity.HasMany(d => d.Roles).WithMany(p => p.UsersNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "Userrole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("userroles_ibfk_2"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("userroles_ibfk_1"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("userroles");
                        j.HasIndex(new[] { "RoleId" }, "role_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<Waitlist>(entity =>
        {
            entity.HasKey(e => e.WaitlistId).HasName("PRIMARY");

            entity.ToTable("waitlist");

            entity.HasIndex(e => e.BookId, "book_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.WaitlistId).HasColumnName("waitlist_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("request_date");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Pending'")
                .HasColumnType("enum('Pending','Fulfilled','Cancelled')")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Waitlists)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("waitlist_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Waitlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("waitlist_ibfk_1");
        });
    }
}
