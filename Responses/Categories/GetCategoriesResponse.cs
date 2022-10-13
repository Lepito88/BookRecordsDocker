namespace BookRecords.Responses.Categories
{
    public class GetCategoriesResponse : BaseResponse
    {
        public int Idcategory { get; set; }
        public string CategoryName { get; set; }
    }
}
