using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [EmailAddress]
        public string? Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? CreatedAt { get; set; } = default!;
        public DateTime? UpdatedAt { get; set; } = default!;

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
