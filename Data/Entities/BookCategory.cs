using System;
using System.Collections.Generic;

namespace BookRecords.Data.Entities
{
    public partial class BookCategory
    {
        public int Idbook { get; set; }
        public int Idcategory { get; set; }

        public virtual Book IdbookNavigation { get; set; } = null!;
        public virtual Category IdcategoryNavigation { get; set; } = null!;
    }
}
