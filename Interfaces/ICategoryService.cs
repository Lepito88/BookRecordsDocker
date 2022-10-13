using BookRecords.Data.Entities;
using BookRecords.Responses.Categories;

namespace BookRecords.Interfaces
{
    public interface ICategoryService
    {
        Task<GetCategoriesResponse> GetCategoriesAsync();
        Task<CategoryResponse> GetCategoryByIdAsync(int id);
        Task<CategoryResponse> UpdateCategoryAsync(int id, Book book);
        Task<CategoryResponse> DeleteCategoryAsync(int id);
    }
}
