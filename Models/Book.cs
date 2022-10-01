using System;
using System.Collections.Generic;

namespace BookRecords.Models
{
    public partial class Book
    {
        public int Idbook { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? ReleaseYear { get; set; }
        public string? Type { get; set; }
        public string? Isbn { get; set; }
    }
}
