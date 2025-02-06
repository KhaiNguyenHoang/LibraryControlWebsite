﻿// <auto-generated />
using System;
using LibaryControlWebsite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibaryControlWebsite.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Bookauthor", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.HasKey("BookId", "AuthorId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "AuthorId" }, "author_id");

                    b.ToTable("bookauthors", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AuthorId"));

                    b.Property<string>("Biography")
                        .HasColumnType("text")
                        .HasColumnName("biography");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("full_name");

                    b.HasKey("AuthorId")
                        .HasName("PRIMARY");

                    b.ToTable("authors", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("BookId"));

                    b.Property<int>("AvailableCopies")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("available_copies")
                        .HasDefaultValueSql("'1'");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("isbn");

                    b.Property<short?>("PublishYear")
                        .HasColumnType("year")
                        .HasColumnName("publish_year");

                    b.Property<string>("Publisher")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("publisher");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.Property<int>("TotalCopies")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("total_copies")
                        .HasDefaultValueSql("'1'");

                    b.HasKey("BookId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CategoryId" }, "category_id");

                    b.HasIndex(new[] { "Isbn" }, "isbn")
                        .IsUnique();

                    b.HasIndex(new[] { "Title" }, "title")
                        .HasAnnotation("MySql:FullTextIndex", true);

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Bookcopy", b =>
                {
                    b.Property<int>("CopyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("copy_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CopyId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<string>("Conditions")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("enum('New','Good','Damaged')")
                        .HasColumnName("conditions")
                        .HasDefaultValueSql("'Good'");

                    b.Property<int>("CopyNumber")
                        .HasColumnType("int")
                        .HasColumnName("copy_number");

                    b.Property<bool?>("IsAvailable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_available")
                        .HasDefaultValueSql("'1'");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("location");

                    b.HasKey("CopyId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "BookId" }, "book_id");

                    b.ToTable("bookcopies", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Bookreview", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("review_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int?>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("ReviewId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "BookId" }, "book_id")
                        .HasDatabaseName("book_id1");

                    b.HasIndex(new[] { "UserId" }, "user_id");

                    b.ToTable("bookreviews", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("category_name");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("CategoryId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CategoryName" }, "category_name")
                        .IsUnique();

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Fine", b =>
                {
                    b.Property<int>("FineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("fine_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("FineId"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("amount");

                    b.Property<int>("LoanId")
                        .HasColumnType("int")
                        .HasColumnName("loan_id");

                    b.Property<bool?>("Paid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("paid")
                        .HasDefaultValueSql("'0'");

                    b.Property<DateOnly?>("PaidDate")
                        .HasColumnType("date")
                        .HasColumnName("paid_date");

                    b.HasKey("FineId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "LoanId" }, "loan_id");

                    b.ToTable("fines", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Loan", b =>
                {
                    b.Property<int>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("loan_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("LoanId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<DateOnly>("DueDate")
                        .HasColumnType("date")
                        .HasColumnName("due_date");

                    b.Property<DateOnly>("LoanDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("loan_date")
                        .HasDefaultValueSql("curdate()");

                    b.Property<DateOnly?>("ReturnDate")
                        .HasColumnType("date")
                        .HasColumnName("return_date");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("enum('Borrowed','Returned','Overdue')")
                        .HasColumnName("status")
                        .HasDefaultValueSql("'Borrowed'");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("LoanId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "BookId" }, "book_id")
                        .HasDatabaseName("book_id2");

                    b.HasIndex(new[] { "UserId" }, "user_id")
                        .HasDatabaseName("user_id1");

                    b.ToTable("loans", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("permission_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PermissionId"));

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("permission_name");

                    b.HasKey("PermissionId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "PermissionName" }, "permission_name")
                        .IsUnique();

                    b.ToTable("permissions", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("reservation_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<DateTime?>("ReservationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("reservation_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("enum('Pending','Fulfilled','Cancelled')")
                        .HasColumnName("status")
                        .HasDefaultValueSql("'Pending'");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("ReservationId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "BookId" }, "book_id")
                        .HasDatabaseName("book_id3");

                    b.HasIndex(new[] { "UserId" }, "user_id")
                        .HasDatabaseName("user_id2");

                    b.ToTable("reservations", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "RoleName" }, "role_name")
                        .IsUnique();

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password_hash")
                        .HasComment("Bcrypt hashed password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("phone");

                    b.Property<int?>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id")
                        .HasDefaultValueSql("'1'");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("enum('Reader','Staff','Admin')")
                        .HasColumnName("user_type")
                        .HasDefaultValueSql("'Reader'");

                    b.HasKey("UserId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Email" }, "email")
                        .IsUnique();

                    b.HasIndex(new[] { "Phone" }, "phone")
                        .IsUnique();

                    b.HasIndex(new[] { "RoleId" }, "role_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Waitlist", b =>
                {
                    b.Property<int>("WaitlistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("waitlist_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("WaitlistId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<DateTime?>("RequestDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("request_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("enum('Pending','Fulfilled','Cancelled')")
                        .HasColumnName("status")
                        .HasDefaultValueSql("'Pending'");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("WaitlistId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "BookId" }, "book_id")
                        .HasDatabaseName("book_id4");

                    b.HasIndex(new[] { "UserId" }, "user_id")
                        .HasDatabaseName("user_id3");

                    b.ToTable("waitlist", (string)null);
                });

            modelBuilder.Entity("Rolepermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int")
                        .HasColumnName("permission_id");

                    b.HasKey("RoleId", "PermissionId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "PermissionId" }, "permission_id");

                    b.ToTable("rolepermissions", (string)null);
                });

            modelBuilder.Entity("Userrole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "RoleId" }, "role_id")
                        .HasDatabaseName("role_id1");

                    b.ToTable("userroles", (string)null);
                });

            modelBuilder.Entity("Bookauthor", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("bookauthors_ibfk_2");

                    b.HasOne("LibaryControlWebsite.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("bookauthors_ibfk_1");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Book", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("books_ibfk_1");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Bookcopy", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Book", "Book")
                        .WithMany("Bookcopies")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("bookcopies_ibfk_1");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Bookreview", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Book", "Book")
                        .WithMany("Bookreviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("bookreviews_ibfk_2");

                    b.HasOne("LibaryControlWebsite.Models.User", "User")
                        .WithMany("Bookreviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("bookreviews_ibfk_1");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Fine", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Loan", "Loan")
                        .WithMany("Fines")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fines_ibfk_1");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Loan", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Book", "Book")
                        .WithMany("Loans")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("loans_ibfk_2");

                    b.HasOne("LibaryControlWebsite.Models.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("loans_ibfk_1");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Reservation", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Book", "Book")
                        .WithMany("Reservations")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("reservations_ibfk_2");

                    b.HasOne("LibaryControlWebsite.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("reservations_ibfk_1");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.User", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("users_ibfk_1");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Waitlist", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Book", "Book")
                        .WithMany("Waitlists")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("waitlist_ibfk_2");

                    b.HasOne("LibaryControlWebsite.Models.User", "User")
                        .WithMany("Waitlists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("waitlist_ibfk_1");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Rolepermission", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("rolepermissions_ibfk_2");

                    b.HasOne("LibaryControlWebsite.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("rolepermissions_ibfk_1");
                });

            modelBuilder.Entity("Userrole", b =>
                {
                    b.HasOne("LibaryControlWebsite.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("userroles_ibfk_2");

                    b.HasOne("LibaryControlWebsite.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("userroles_ibfk_1");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Book", b =>
                {
                    b.Navigation("Bookcopies");

                    b.Navigation("Bookreviews");

                    b.Navigation("Loans");

                    b.Navigation("Reservations");

                    b.Navigation("Waitlists");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Loan", b =>
                {
                    b.Navigation("Fines");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("LibaryControlWebsite.Models.User", b =>
                {
                    b.Navigation("Bookreviews");

                    b.Navigation("Loans");

                    b.Navigation("Reservations");

                    b.Navigation("Waitlists");
                });
#pragma warning restore 612, 618
        }
    }
}
