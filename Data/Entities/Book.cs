using System;
using System.Collections.Generic;

namespace BookRecords.Data.Entities
{
    public partial class Book
    {
        public Book()
        {
            Idauthors = new HashSet<Author>();
            Idcategories = new HashSet<Category>();
            Idusers = new HashSet<User>();
        }

        public int Idbook { get; set; }
        public string BookName { get; set; } = null!;
        public DateTime? ReleaseYear { get; set; }
        public string? Type { get; set; }
        public string? Isbn { get; set; }

        public virtual ICollection<Author> Idauthors { get; set; }
        public virtual ICollection<Category> Idcategories { get; set; }
        public virtual ICollection<User> Idusers { get; set; }
    }
}
