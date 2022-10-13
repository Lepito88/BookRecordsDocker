using System;
using System.Collections.Generic;

namespace BookRecords.Data.Entities
{
    public partial class User
    {
        public User()
        {
            Books = new HashSet<Book>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public int Iduser { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
