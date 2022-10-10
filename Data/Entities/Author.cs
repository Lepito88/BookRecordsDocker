using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookRecords.Data.Entities
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Idauthor { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}
