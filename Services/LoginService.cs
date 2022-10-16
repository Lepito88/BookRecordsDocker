using BookRecords.Data;
using BookRecords.Data.Entities;
using BookRecords.Interfaces;
using BookRecords.Requests;
using BookRecords.Responses;
using BookRecords.Responses.Token;
using Microsoft.EntityFrameworkCore;

namespace BookRecords.Services
{
    public class LoginService : ILoginService
    {
        private readonly bookrecordsContext _context;
        private readonly ITokenService _tokenService;

        public LoginService(bookrecordsContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
        {
            //Find user
            var user = await _context.Users
                .Where(x => x.Username == loginRequest.Username || x.Email == loginRequest.Email)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Username or Email not found",
                    ErrorCode = "L01",
                };
            }
            var verified = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);

            if (!verified)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Invalid Password",
                    ErrorCode = "L02"

                };
            }
            var token = await System.Threading.Tasks.Task.Run(() => _tokenService.GenerateTokensAsync(user));
            return new TokenResponse
            {
                Success = true,
                AccessToken = token.Item1,
                RefreshToken = token.Item2
            };
        }

        public async Task<LogoutResponse> LogoutAsync(int Iduser)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(o => o.Iduser == Iduser);

            if (refreshToken is null)
            {
                return new LogoutResponse { Success = true };
            }
            _context.RefreshTokens.Remove(refreshToken);

            var saveResponse = await _context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new LogoutResponse { Success = true };
            }
            return new LogoutResponse
            {
                Success = false,
                Error = "Unable to logout user",
                ErrorCode = "L03"
            };
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(user => user.Email == registerRequest.Email || user.Username == registerRequest.Username);

            if (existingUser != null)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Error = "User already exists with same Email or Username",
                    ErrorCode = "R01"
                };
            }
            if (registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Error = "Password and confirm password do not match",
                    ErrorCode = "R02"
                };
            }
            if (registerRequest.Password.Length <= 7)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Error = "Password is weak",
                    ErrorCode = "R03"
                };
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
            var newUser = new User
            {
                Email = registerRequest.Email,
                Password = passwordHash,
                Username = registerRequest.Username,
                Firstname = registerRequest.Firstname,
                Lastname = registerRequest.Lastname,
            };
            await _context.Users.AddAsync(newUser);

            var saveResponse = await _context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new RegisterResponse
                {
                    Success = true,
                    Email = newUser.Email,
                    Username = newUser.Username,
                };
            }
            return new RegisterResponse
            {
                Success = false,
                Error = "Unable to save user",
                ErrorCode = "R04"
            };
        }
    }
}
//Used ERRORCODES
//L01 : Username or Email not found
//L02 : Invalid Password
//L03 : Unable to logout user
//R01 : User already exists with same Email or Username
//R02 : Password and confirm password do not match
//R03 : Password is weak
//R04 : Unabke to save user