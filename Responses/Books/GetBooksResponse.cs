using BookRecords.Data.Entities;

namespace BookRecords.Responses.Books
{
    public class GetBooksResponse : BaseResponse
    {
        public List<Book> Books { get; set; }
    }
}
