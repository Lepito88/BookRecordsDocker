using BookRecords.Requests;
using BookRecords.Responses;
using BookRecords.Responses.Token;

namespace BookRecords.Interfaces
{
    public interface IUserService
    {
        Task<TokenResponse> LoginAsync(LoginRequest loginRequest);
        Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<LogoutResponse> LogoutAsync(int Iduser);
    }
}
