using BookRecords.Data.Entities;

namespace BookRecords.Responses.Users
{
    public class GetUsersResponse : BaseResponse
    {
        public List<User> Users { get; set; }
    }
}
