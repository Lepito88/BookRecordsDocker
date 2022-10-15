using BookRecords.Data.Entities;

namespace BookRecords.Responses.Categories
{
    public class GetCategoriesResponse : BaseResponse
    {
        public List<Category> Categories { get; set; }
    }
}
