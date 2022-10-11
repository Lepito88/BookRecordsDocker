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
            try
            {
                //Find user
                var user = await _context.Users
                    .Where(x => x.Username == login.username)
                    .FirstOrDefaultAsync();

                if (user is null)
                {
                    return new OkObjectResult(false);
                }
                return !BCrypt.Net.BCrypt.Verify(login.password, user.Password) ? new OkObjectResult(false) : (IActionResult)new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
           
        }
    }
}
