using BookRecords.Data.Entities;
using BookRecords.Requests;
using BookRecords.Responses.Token;

namespace BookRecords.Interfaces
{
    public interface ITokenService
    {
        Task<Tuple<string, string>> GenerateTokensAsync(int Iduser);
        Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task<bool> RemoveRefreshTokenAsync(User user);
    }
}
