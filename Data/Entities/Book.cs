using System;
using System.Collections.Generic;

namespace BookRecords.Data.Entities
{
    public partial class Book
    {
        public int Idbook { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? ReleaseYear { get; set; }
        public string? Type { get; set; }
        public string? Isbn { get; set; }

        public virtual AuthorBook? AuthorBook { get; set; }
        public virtual BookCategory? BookCategory { get; set; }
        public virtual UserBook? UserBook { get; set; }
    }
}
