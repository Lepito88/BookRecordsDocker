using BookRecords.Data.Entities;
using BookRecords.Interfaces;
using BookRecords.Responses.Categories;

namespace BookRecords.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<CategoryResponse> CreateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetCategoriesResponse> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse> UpdateCategoryAsync(int id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
