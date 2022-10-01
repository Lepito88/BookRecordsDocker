﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookRecords.Models
{
    public partial class book_recordsContext : DbContext
    {
        public book_recordsContext()
        {
        }

        public book_recordsContext(DbContextOptions<book_recordsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<AuthorBook> AuthorBooks { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookCategory> BookCategories { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserBook> UserBooks { get; set; } = null!;

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
            });

            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("author_books");

                entity.HasIndex(e => e.Idauthor, "fk_author_authorbooks_idx");

                entity.HasIndex(e => e.Idbook, "fk_book_authorbooks_idx");

                entity.Property(e => e.Idauthor).HasColumnName("idauthor");

                entity.Property(e => e.Idbook).HasColumnName("idbook");

                entity.HasOne(d => d.IdauthorNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idauthor)
                    .HasConstraintName("fk_author_authorbooks");

                entity.HasOne(d => d.IdbookNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idbook)
                    .HasConstraintName("fk_book_authorbooks");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Idbook)
                    .HasName("PRIMARY");

                entity.ToTable("book");

                entity.Property(e => e.Idbook).HasColumnName("idbook");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(255)
                    .HasColumnName("isbn");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ReleaseYear)
                    .HasColumnType("datetime")
                    .HasColumnName("release_year");

                entity.Property(e => e.Type)
                    .HasColumnType("enum('Sidottu','Nidottu','Digitaalinen')")
                    .HasColumnName("type");
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("book_categories");

                entity.HasIndex(e => e.Idbook, "fk_book_bookcategories_idx");

                entity.HasIndex(e => e.Idcategory, "fk_category_bookcategories_idx");

                entity.Property(e => e.Idbook).HasColumnName("idbook");

                entity.Property(e => e.Idcategory).HasColumnName("idcategory");

                entity.HasOne(d => d.IdbookNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idbook)
                    .HasConstraintName("fk_book_bookcategories");

                entity.HasOne(d => d.IdcategoryNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idcategory)
                    .HasConstraintName("fk_category_bookcategories");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Idcategory)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.Property(e => e.Idcategory)
                    .ValueGeneratedNever()
                    .HasColumnName("idcategory");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
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
            });

            modelBuilder.Entity<UserBook>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_books");

                entity.HasIndex(e => e.Idbook, "fk_book_userbooks_idx");

                entity.HasIndex(e => e.Iduser, "fk_user_userbooks_idx");

                entity.Property(e => e.Idbook).HasColumnName("idbook");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.HasOne(d => d.IdbookNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idbook)
                    .HasConstraintName("fk_book_userbooks");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Iduser)
                    .HasConstraintName("fk_user_userbooks");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
