using BookRecords.Data.Entities;
using BookRecords.Responses.Books;

namespace BookRecords.Responses.Authors
{
    public class AuthorResponse : BaseResponse
    {
        public int Idauthor { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<BooksResponseForAuthor> Books { get; set; }
    }
}
