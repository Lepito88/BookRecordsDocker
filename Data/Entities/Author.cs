using System;
using System.Collections.Generic;

namespace BookRecords.Data.Entities
{
    public partial class Author
    {
        public int Idauthor { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public virtual AuthorBook? AuthorBook { get; set; }
    }
}
