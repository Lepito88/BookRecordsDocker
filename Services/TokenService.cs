using BookRecords.Data;
using BookRecords.Data.Entities;
using BookRecords.Helpers;
using BookRecords.Interfaces;
using BookRecords.Requests;
using BookRecords.Responses.Token;
using Microsoft.EntityFrameworkCore;

namespace BookRecords.Services
{
    public class TokenService : ITokenService
    {

        private readonly BookRecordsContext _context;
        private readonly IConfiguration _configuration;

        public TokenService(BookRecordsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Tuple<string, string>> GenerateTokensAsync(User user)
        {
            var userRecord = await _context.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync(e => e.Iduser == user.Iduser);
            
            if (userRecord == null)
            {
                return null;
            }
            var tokenHelper = new TokenHelper(_configuration);
            var accessToken = await tokenHelper.GenerateAccessToken(userRecord);
            var refreshToken = await TokenHelper.GenerateRefreshToken();


            var salt = HashHelper.GetSecureSalt();

            var refreshTokenHashed = HashHelper.HashUsingPbkdf2(refreshToken, salt);

            if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                await RemoveRefreshTokenAsync(userRecord);

            }
            userRecord.RefreshTokens?.Add(new RefreshToken
            {
                ExpiryDate = DateTime.Now.AddDays(30),
                Timestamp = DateTime.Now,
                Iduser = user.Iduser,
                TokenHash = refreshTokenHashed,
                TokenSalt = Convert.ToBase64String(salt)

            });

            await _context.SaveChangesAsync();

            var token = new Tuple<string, string>(accessToken, refreshToken);

            return token;
        }

        public async Task<bool> RemoveRefreshTokenAsync(User user)
        {
            var userRecord = await _context.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync(e => e.Iduser == user.Iduser);

            if (userRecord == null)
            {
                return false;
            }

            if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                var currentRefreshToken = userRecord.RefreshTokens.First();

                _context.RefreshTokens.Remove(currentRefreshToken);
            }

            return false;
        }

        public async Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(o => o.Iduser == refreshTokenRequest.Iduser);

            var response = new ValidateRefreshTokenResponse();
            if (refreshToken == null)
            {
                response.Success = false;
                response.Error = "Invalid session or user is already logged out";
                response.ErrorCode = "R02";
                return response;
            }

            var refreshTokenToValidateHash = HashHelper.HashUsingPbkdf2(refreshTokenRequest.RefreshToken, Convert.FromBase64String(refreshToken.TokenSalt));

            if (refreshToken.TokenHash != refreshTokenToValidateHash)
            {
                response.Success = false;
                response.Error = "Invalid refresh token";
                response.ErrorCode = "R03";
                return response;
            }

            if (refreshToken.ExpiryDate < DateTime.Now)
            {
                response.Success = false;
                response.Error = "Refresh token has expired";
                response.ErrorCode = "R04";
                return response;
            }

            response.Success = true;
            response.Iduser = refreshToken.Iduser;

            return response;
        }

    }
}
