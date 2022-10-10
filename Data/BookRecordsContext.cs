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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Idauthor)
                    .HasName("PRIMARY");

                entity.ToTable("author");

                entity.Property(e => e.Idauthor).HasColumnName("idauthor");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");

                entity.HasMany(d => d.Books)
                    .WithMany(p => p.Authors)
                    .UsingEntity<Dictionary<string, object>>(
                        "AuthorBook",
                        l => l.HasOne<Book>().WithMany().HasForeignKey("Idbook").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_book_authorbooks"),
                        r => r.HasOne<Author>().WithMany().HasForeignKey("Idauthor").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_author_authorbooks"),
                        j =>
                        {
                            j.HasKey("Idauthor", "Idbook").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("author_books");

                            j.HasIndex(new[] { "Idauthor" }, "fk_author_authorbooks_idx");

                            j.HasIndex(new[] { "Idbook" }, "fk_book_authorbooks_idx");

                            j.IndexerProperty<int>("Idauthor").HasColumnName("idauthor");

                            j.IndexerProperty<int>("Idbook").HasColumnName("idbook");
                        });
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Idbook)
                    .HasName("PRIMARY");

                entity.ToTable("book");

                entity.Property(e => e.Idbook).HasColumnName("idbook");

                entity.Property(e => e.BookName)
                    .HasMaxLength(255)
                    .HasColumnName("book_name");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .HasColumnName("isbn");

                entity.Property(e => e.ReleaseYear).HasColumnName("release_year");

                entity.Property(e => e.Type)
                    .HasColumnType("enum('Hardcover','Paperback','Digital','Comicbook')")
                    .HasColumnName("type");

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("Idcategory").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_category_bookcategories"),
                        r => r.HasOne<Book>().WithMany().HasForeignKey("Idbook").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_book_bookcategories"),
                        j =>
                        {
                            j.HasKey("Idbook", "Idcategory").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("book_categories");

                            j.HasIndex(new[] { "Idbook" }, "fk_book_bookcategories_idx");

                            j.HasIndex(new[] { "Idcategory" }, "fk_category_bookcategories_idx");

                            j.IndexerProperty<int>("Idbook").HasColumnName("idbook");

                            j.IndexerProperty<int>("Idcategory").HasColumnName("idcategory");
                        });
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Idcategory)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.Property(e => e.Idcategory)
                    .ValueGeneratedNever()
                    .HasColumnName("idcategory");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.Username, "username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.HasMany(d => d.Books)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserBook",
                        l => l.HasOne<Book>().WithMany().HasForeignKey("Idbook").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_book_userbooks"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("Iduser").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_user_userbooks"),
                        j =>
                        {
                            j.HasKey("Iduser", "Idbook").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("user_books");

                            j.HasIndex(new[] { "Idbook" }, "fk_book_userbooks_idx");

                            j.HasIndex(new[] { "Iduser" }, "fk_user_userbooks_idx");

                            j.IndexerProperty<int>("Iduser").HasColumnName("iduser");

                            j.IndexerProperty<int>("Idbook").HasColumnName("idbook");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
