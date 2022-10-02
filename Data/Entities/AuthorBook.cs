using System;
using System.Collections.Generic;

namespace BookRecords.Data.Entities
{
    public partial class AuthorBook
    {
        public int Idauthor { get; set; }
        public int Idbook { get; set; }

        public virtual Author IdauthorNavigation { get; set; } = null!;
        public virtual Book IdbookNavigation { get; set; } = null!;
    }
}
