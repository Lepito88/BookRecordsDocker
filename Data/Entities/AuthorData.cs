namespace BookRecords.Data.Entities
{
    public class AuthorData
    {
        public AuthorData()
        {
            Books = new HashSet<Book>();
        }

        public int Idauthor { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }

        public string GetFullname()
        {
            return Firstname + ' ' + Lastname;
        }
    }
}
