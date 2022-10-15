using BookRecords.Requests;
using BookRecords.Responses.Token;
using BookRecords.Responses;

namespace BookRecords.Interfaces
{
    public interface ILoginService
    {
        Task<TokenResponse> LoginAsync(LoginRequest loginRequest);
        Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<LogoutResponse> LogoutAsync(int Iduser);
    }
}
