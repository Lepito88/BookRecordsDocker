using System;
using System.Collections.Generic;
using BookRecords.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookRecords.Data
{
    public partial class bookrecordsContext : DbContext
    {
        public bookrecordsContext()
        {
        }

        public bookrecordsContext(DbContextOptions<bookrecordsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.IdRefreshToken)
                    .HasName("PRIMARY");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.TokenHash)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.TokenSalt)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_User");

                entity.ToTable("RefreshToken");
            });
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Idauthor)
                    .HasName("PRIMARY");

                entity.ToTable("Author");

                entity.Property(e => e.Idauthor).HasColumnName("Idauthor");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("Firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("Lastname");

                entity.HasMany(d => d.Books)
                    .WithMany(p => p.Authors)
                    .UsingEntity<Dictionary<string, object>>(
                        "AuthorBook",
                        l => l.HasOne<Book>().WithMany().HasForeignKey("Idbook").OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_book_authorbooks"),
                        r => r.HasOne<Author>().WithMany().HasForeignKey("Idauthor").OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_author_authorbooks"),
                        j =>
                        {
                            j.HasKey("Idauthor", "Idbook").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("AuthorBooks");

                            j.HasIndex(new[] { "Idauthor" }, "fk_author_authorbooks_idx");

                            j.HasIndex(new[] { "Idbook" }, "fk_book_authorbooks_idx");

                            j.IndexerProperty<int>("Idauthor").HasColumnName("Idauthor");

                            j.IndexerProperty<int>("Idbook").HasColumnName("Idbook");
                        });
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Idbook)
                    .HasName("PRIMARY");

                entity.ToTable("Book");

                entity.Property(e => e.Idbook).HasColumnName("Idbook");

                entity.Property(e => e.BookName)
                    .HasMaxLength(255)
                    .HasColumnName("BookName");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .HasColumnName("Isbn");

                entity.Property(e => e.ReleaseYear).HasColumnName("ReleaseYear");

                entity.Property(e => e.Type)
                    .HasColumnType("enum('Hardcover','Paperback','Digital','Comicbook')")
                    .HasColumnName("Type");

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("Idcategory").OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_category_bookcategories"),
                        r => r.HasOne<Book>().WithMany().HasForeignKey("Idbook").OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_book_bookcategories"),
                        j =>
                        {
                            j.HasKey("Idbook", "Idcategory").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("BookCategories");

                            j.HasIndex(new[] { "Idbook" }, "fk_book_bookcategories_idx");

                            j.HasIndex(new[] { "Idcategory" }, "fk_category_bookcategories_idx");

                            j.IndexerProperty<int>("Idbook").HasColumnName("Idbook");

                            j.IndexerProperty<int>("Idcategory").HasColumnName("Idcategory");
                        });
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Idcategory)
                    .HasName("PRIMARY");

                entity.ToTable("Category");

                entity.Property(e => e.Idcategory)
                    .ValueGeneratedNever()
                    .HasColumnName("Idcategory");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .HasColumnName("CategoryName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PRIMARY");

                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "Username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Iduser).HasColumnName("Iduser");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("Email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("Firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("Lastname");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("Password");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("Username");

                entity.HasMany(d => d.Books)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserBook",
                        l => l.HasOne<Book>().WithMany().HasForeignKey("Idbook").OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_book_userbooks"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("Iduser").OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_user_userbooks"),
                        j =>
                        {
                            j.HasKey("Iduser", "Idbook").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("UserBooks");

                            j.HasIndex(new[] { "Idbook" }, "fk_book_userbooks_idx");

                            j.HasIndex(new[] { "Iduser" }, "fk_user_userbooks_idx");

                            j.IndexerProperty<int>("Iduser").HasColumnName("Iduser");

                            j.IndexerProperty<int>("Idbook").HasColumnName("Idbook");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
