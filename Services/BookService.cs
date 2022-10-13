using BookRecords.Data.Entities;
using BookRecords.Interfaces;
using BookRecords.Responses.Books;

namespace BookRecords.Services
{
    public class BookService : IBookService
    {
        public Task<BookResponse> DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BookResponse> GetBookByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetBooksResponse> GetBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookResponse> UpdateBookAsync(int id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
