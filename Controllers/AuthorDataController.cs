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
    public class AuthorDataController : ControllerBase
    {
        private readonly bookrecordsContext _context;

        public AuthorDataController(bookrecordsContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorData>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            var authorList = new List<AuthorData>();

            foreach (var author in authors)
            {
                authorList.Add(AuthorToAuthorData(author));
            }

            return authorList;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorData>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .Where(_ => _.Idauthor == id)
                .IgnoreAutoIncludes()
                .Include(_ => _.Books)
                .FirstOrDefaultAsync();
            //.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }
           
            return AuthorToAuthorData(author);
        }

        private static AuthorData AuthorToAuthorData(Author author) =>
        new AuthorData
        {
            Idauthor = author.Idauthor,
            Firstname = author.Firstname,
            Lastname = author.Lastname,
            Books = author.Books,
        };

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Idauthor == id);
        }
    }
}
