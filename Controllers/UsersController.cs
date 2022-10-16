using Microsoft.AspNetCore.Mvc;
using BookRecords.Data.Entities;
using BookRecords.Interfaces;
using BookRecords.Responses.Users;
using BookRecords.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookRecords.Controllers
{
    //This controller contains basig CRUD METHODS for User class, (For the admin)
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var getUsersResponse = await _userService.GetUsersAsync();

            if (!getUsersResponse.Success)
            {
                return BadRequest();
            }
            var userResponse = getUsersResponse.Users.ConvertAll(o => new UserResponse
            {
                Iduser = o.Iduser,
                Username = o.Username,
                Firstname = o.Firstname,
                Lastname = o.Lastname,
                Email = o.Email,
            });

            return Ok(userResponse);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);             
            if (user == null)
            {
                return NotFound();
            }
            var userResponse = new UserDetailResponse {
                Iduser = user.Iduser,
                Username=user.Username,
                Email = user.Email,
                Firstname=user.Firstname,
                Lastname=user.Lastname,
                CreatedAt=user.CreatedAt,
                Books = user.Books,
            };

            return Ok(userResponse);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, User user)
        {
            if (id != user.Iduser)
            {
                return BadRequest();
            }

            var userUpdateResponse = await _userService.UpdateUserAsync(id, user);

            if (!userUpdateResponse.Success)
            {
                return BadRequest(user);
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostUser(User user)
        {
            var userResponse = await _userService.CreateUserAsync(user);

            if (!userResponse.Success)
            {
                return UnprocessableEntity(user);
            }

            return CreatedAtAction(nameof(GetUser), new { id = user.Iduser }, user);
            
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var userResponse = await _userService.DeleteUserAsync(id);
            if (!userResponse.Success)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }
}
