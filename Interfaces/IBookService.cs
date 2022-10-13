using BookRecords.Data.Entities;
using BookRecords.Responses.Books;

namespace BookRecords.Interfaces
{
    public interface IBookService
    {
        Task<GetBooksResponse> GetBooksAsync();
        Task<BookResponse> GetBookByIdAsync(int id);
        Task<BookResponse> UpdateBookAsync(int id, Book book);
        Task<BookResponse> DeleteBookAsync(int id);
    }
}
