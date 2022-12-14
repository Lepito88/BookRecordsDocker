using BookRecords.Data.Entities;

namespace BookRecords.Responses.Users
{
    public class UserDetailResponse : BaseResponse
    {
        public int Iduser { get; set; } 
        public string Username { get; set; } = null!;
        public string? Email { get; set; } 
        public string? Firstname { get; set; } 
        public string? Lastname { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
