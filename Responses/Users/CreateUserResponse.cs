namespace BookRecords.Responses.Users
{
    public class CreateUserResponse : BaseResponse
    {
        public int Iduser { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; } = string.Empty;
    }
}
