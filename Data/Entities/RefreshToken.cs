namespace BookRecords.Data.Entities
{
    public partial class RefreshToken
    {

        public int IdRefreshToken { get; set; }

        public int Iduser { get; set; }

        public string TokenHash { get; set; }

        public string TokenSalt { get; set; }

        public DateTime Timestamp { get; set; }

        public DateTime ExpiryDate { get; set; }

        public virtual User User { get; set; }

    }

}
