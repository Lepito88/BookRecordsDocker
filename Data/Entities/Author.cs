using System;
using System.Collections.Generic;

namespace BookRecords.Data.Entities
{
    public partial class Author
    {
        public Author()
        {
            Idbooks = new HashSet<Book>();
        }

        public int Idauthor { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;

        public virtual ICollection<Book> Idbooks { get; set; }
    }
}
