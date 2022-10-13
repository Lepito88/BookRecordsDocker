using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookRecords.Data;
using BookRecords.Data.Entities;

namespace BookRecords.Controllers
{
    //This controller contains basig CRUD METHODS for User class, (For the admin)
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly bookrecordsContext _context;

        public UsersController(bookrecordsContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);             
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            //check if id in url is same as iduser in user object
            if (id != user.Iduser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //If user exist (check if username exist), return badrequest
            if (UsernameExists(user.Username))
            {
                return Content("Username already exists");
            }
            else
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUser), new { id = user.Iduser }, user);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Iduser == id);
        }
        //
        private bool UsernameExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }
    }
}
