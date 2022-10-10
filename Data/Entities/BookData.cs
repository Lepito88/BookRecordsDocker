using System.Text.Json.Serialization;

namespace BookRecords.Data.Entities
{
    public class BookData
    {
        public BookData()
        {
            Authors = new HashSet<Author>();
            Categories = new HashSet<Category>();
            Users = new HashSet<User>();
        }

        public int Idbook { get; set; }
        public string BookName { get; set; } = null!;
        public DateTime? ReleaseYear { get; set; }
        public string? Type { get; set; }
        public string? Isbn { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
