using System;
using System.Collections.Generic;

namespace BookRecords.Models
{
    public partial class UserBook
    {
        public int Iduser { get; set; }
        public int Idbook { get; set; }

        public virtual Book IdbookNavigation { get; set; } = null!;
        public virtual User IduserNavigation { get; set; } = null!;
    }
}
