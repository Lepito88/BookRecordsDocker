using BookRecords.Data.Entities;
using BookRecords.Responses.Categories;

namespace BookRecords.Interfaces
{
    public interface ICategoryService
    {
        Task<GetCategoriesResponse> GetCategoriesAsync();
        Task<CategoryResponse> GetCategoryByIdAsync(int id);
        Task<CategoryResponse> CreateCategoryAsync(Category category);
        Task<CategoryResponse> UpdateCategoryAsync(int id, Category category);
        Task<CategoryResponse> DeleteCategoryAsync(int id);
    }
}
