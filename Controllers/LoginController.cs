using BookRecords.Data;
using BookRecords.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly bookrecordsContext _context;

        public LoginController(bookrecordsContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            //Find user
            var user = await _context.Users
                .Where(x => x.Username == login.username)
                .FirstAsync();

            if (user is null || !BCrypt.Net.BCrypt.Verify(login.password, user.Password))
            {
                return new OkObjectResult(false);
            }else
            {
                return new OkObjectResult(true);
            }
        }
    }
}
