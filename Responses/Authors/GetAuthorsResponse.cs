using BookRecords.Data.Entities;

namespace BookRecords.Responses.Authors
{
    public class GetAuthorsResponse : BaseResponse
    {
        public List<Author> Authors { get; set; }
    }
}
