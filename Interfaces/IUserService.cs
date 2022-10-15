using BookRecords.Data.Entities;
using BookRecords.Requests;
using BookRecords.Responses;
using BookRecords.Responses.Categories;
using BookRecords.Responses.Token;
using BookRecords.Responses.Users;

namespace BookRecords.Interfaces
{
    public interface IUserService
    {
        Task<GetUsersResponse> GetUsersAsync();
        Task<UserDetailResponse> GetUserByIdAsync(int id);
        Task<CreateUserResponse> CreateUserAsync(User user);
        Task<CreateUserResponse> UpdateUserAsync(int id, User user);
        Task<CreateUserResponse> DeleteUserAsync(int id);
    }
}
