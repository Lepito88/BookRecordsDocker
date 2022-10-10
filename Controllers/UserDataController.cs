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
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly bookrecordsContext _context;

        public UserDataController(bookrecordsContext context)
        {
            _context = context;
        }
        // GET: api/Userdata
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();


            var userDataList = new List<UserData>();

            foreach (var user in users)
            {
                userDataList.Add(UserToUserData(user));
            }

            return userDataList;
        }
        // GET: api/UserData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUser(int id)
        {
            var user = await _context.Users
                .Where(b => b.Iduser == id)
                .Include(x => x.Books)
                .ThenInclude(a => a.Authors)
                .ThenInclude(_ => _.Books)
                .ThenInclude(_ => _.Categories)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            return UserToUserData(user);
        }
        //[HttpPost("{id}/book")]
        //public async Task<ActionResult<Book>> CreateUserBook(int id, Book book)
        //{
        //    //find user by id
        //    var user = await _context.Users.FindAsync(id);

        //    //get authors
        //    var authors = await _context.Authors.ToListAsync();
        //    //get categories
        //    var categories = await _context.Categories.ToListAsync();



        //    //create new book
        //    var userbook = new Book
        //    {
        //        BookName = book.BookName,
        //        ReleaseYear = book.ReleaseYear,
        //        Type = book.Type,
        //        Isbn = book.Isbn,
        //        Authors = book.Authors,
        //        Categories = book.Categories,
        //    };

        //    return book;
        //}

        //ADD BOOK TO USER
        [HttpPut("{iduser}/book/{idbook}/add")]
        public async Task<IActionResult> PutBookToUser(int iduser,int idbook)
        {
            //check if id in url is same as iduser in user object

            //get user
            var user = await _context.Users.FindAsync(iduser);
            if (user == null) { return NotFound("User not found"); }

            //find book
            var book = await _context.Books.FindAsync(idbook);
            if (book == null) { return NotFound("Book not found"); }

            //Add book to user
            user.Books.Add(book);

            //mark user state to be changed
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(iduser))
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

        //REMOVE BOOK FROM USER
        [HttpPut("{iduser}/book/{idbook}/remove")]
        public async Task<IActionResult> RemoveBookFromUser(int iduser, int idbook)
        {
            //check if id in url is same as iduser in user object

            //get user
            var user = await _context.Users
                .Where(_ => _.Iduser == iduser)
                .Include(_ => _.Books)
                .FirstOrDefaultAsync();
            if (user == null) { return NotFound("User not found"); }

            //find book
            var book = await _context.Books.FindAsync(idbook);
            if (book == null) { return NotFound("Book not found"); }

            //Remove book from user
            user.Books.Remove(book);

            //mark user state to be changed
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(iduser))
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Iduser == id);
        }
        private static UserData UserToUserData(User user) =>
            new UserData
            {
                Iduser = user.Iduser,
                Username = user.Username,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Books = user.Books,
            };
    }
}
