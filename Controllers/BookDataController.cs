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
    public class BookDataController : ControllerBase
    {
        private readonly bookrecordsContext _context;

        public BookDataController(bookrecordsContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _context.Books
                .Include(_ => _.Authors)
                .Include(_ => _.Categories)
                .ToListAsync();
            return books;

        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books
                .Where(_ => _.Idbook == id)
                .Include(_ => _.Authors)
                .Include(_ => _.Categories)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
    
        // PUT: api/BookData/5/Author/1/add
        [HttpPut("{idbook}/author/{idauthor}/add")]
        public async Task<IActionResult> AddAuthorToBook(int idbook, int idauthor )
        {
           var book = await _context.Books
                .Where(book => book.Idbook == idbook)
                .Include(_ => _.Authors)
                .FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound("Book not found");
            }
            // find author
            var author = await _context.Authors.FindAsync(idauthor);

            if (author == null)
            {
                return NotFound("Author not found");
            }
            //Add Author to Book
            book.Authors.Add(author);

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(idbook))
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

        // PUT: api/BookData/5/Author/1/remove
        [HttpPut("{idbook}/author/{idauthor}/remove")]
        public async Task<IActionResult> RemoveAuthorFromBook(int idbook, int idauthor)
        {
            //find book
            var book = await _context.Books
                 .Where(book => book.Idbook == idbook)
                 .Include(_ => _.Authors)
                 .FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound("Book not found");
            }
            // find author
            var author = await _context.Authors.FindAsync(idauthor);

            if (author == null)
            {
                return NotFound("Author not found");
            }
            //remove Author from Book
            book.Authors.Remove(author);

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(idbook))
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
        // PUT: api/BookData/5/Category/1/add
        [HttpPut("{idbook}/category/{idcategory}/add")]
        public async Task<IActionResult> AddCategoryToBook(int idbook, int idcategory)
        {
            var book = await _context.Books
                 .Where(book => book.Idbook == idbook)
                 .Include(_ => _.Categories)
                 .FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound("Book not found");
            }
            // find Category
            var category = await _context.Categories.FindAsync(idcategory);

            if (category == null)
            {
                return NotFound("Category not found");
            }
            //Add Author to Book
            book.Categories.Add(category);

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(idbook))
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
        // PUT: api/BookData/5/Category/1/remove
        [HttpPut("{idbook}/category/{idcategory}/remove")]
        public async Task<IActionResult> RemoveCategoryFromBook(int idbook, int idcategory)
        {
            var book = await _context.Books
                 .Where(book => book.Idbook == idbook)
                 .Include(_ => _.Categories)
                 .FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound("Book not found");
            }
            // find Category
            var category = await _context.Categories.FindAsync(idcategory);

            if (category == null)
            {
                return NotFound("Category not found");
            }
            //Add Author to Book
            book.Categories.Remove(category);

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(idbook))
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

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Idbook == id);
        }
        private static BookData BookToBookData(Book book) =>
        new BookData
        {
            Idbook = book.Idbook,
            BookName = book.BookName,
            Type = book.Type,
            ReleaseYear = book.ReleaseYear,
            Isbn = book.Isbn,
            Authors = book.Authors,
            Categories = book.Categories,
        };
    }
}
