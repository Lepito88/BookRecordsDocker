using System;
using System.Collections.Generic;

namespace BookRecords.Data.Entities
{
    public partial class UserData
    {
        public UserData()
        {
            Books = new HashSet<Book>();
        }

        public int Iduser { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
       

        public virtual ICollection<Book> Books { get; set; }
    }
}
