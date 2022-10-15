namespace BookRecords.Responses
{
    public class BookToUserResponse : BaseResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
    }
}
