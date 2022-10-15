using BookRecords.Data.Entities;
using BookRecords.Responses;

namespace BookRecords.Interfaces
{
    public interface IEntityRelationShipManagerService
    {
        public Task<BookToUserResponse> AddBookToUserAsync(int userid, int bookid);
        public Task<BookToUserResponse> RemoveBookFromUserAsync(int userid, int bookid);

        public Task<CategoryToBookResponse> AddCategoryToBookAsync(int bookid, int categoryid);
        public Task<CategoryToBookResponse> RemoveCategoryFromBookAsync(int bookid, int categoryid);

        public Task<AuthorToBookResponse> AddAuthorToBookAsync(int bookid, int authorid);
        public Task<AuthorToBookResponse> RemoveAuthorFromBookAsync(int bookid, int authorid);
    }
}
