using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookRecords.Data.Entities
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int Idcategory { get; set; }
        public string CategoryName { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}
