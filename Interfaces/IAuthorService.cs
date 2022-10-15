using BookRecords.Data.Entities;
using BookRecords.Responses.Authors;

namespace BookRecords.Interfaces
{
    public interface IAuthorService
    {
        Task<GetAuthorsResponse> GetAuthorsAsync();
        Task<AuthorResponse> GetAuthorByIdAsync(int id);
        Task<AuthorResponse> CreateAuthorAsync(Author author);
        Task<AuthorResponse> UpdateAuthorAsync(int id, Author author);
        Task<AuthorResponse> DeleteAuthorAsync(int id);
    }
}
