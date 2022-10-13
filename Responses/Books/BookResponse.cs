using BookRecords.Data.Entities;

namespace BookRecords.Responses.Books
{
    public class BookResponse : BaseResponse
    {
        public int Idbook { get; set; }
        public string BookName { get; set; }
        public string Type { get; set; }
        public string Isbn { get; set; }
        public DateTime ReleaseYear { get; set; }

        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }
    }
}
