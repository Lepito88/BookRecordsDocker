using BookRecords.Interfaces;
using BookRecords.Requests;
using BookRecords.Responses;
using BookRecords.Responses.Token;

namespace BookRecords.Services
{
    public class UserService : IUserService
    {
        public Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<LogoutResponse> LogoutAsync(int Iduser)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
