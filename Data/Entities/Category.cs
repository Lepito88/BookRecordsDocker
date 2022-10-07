using System;
using System.Collections.Generic;

namespace BookRecords.Data.Entities
{
    public partial class Category
    {
        public Category()
        {
            Idbooks = new HashSet<Book>();
        }

        public int Idcategory { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Book> Idbooks { get; set; }
    }
}
