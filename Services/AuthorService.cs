using BookRecords.Data;
using BookRecords.Data.Entities;
using BookRecords.Interfaces;
using BookRecords.Responses.Authors;
using Microsoft.EntityFrameworkCore;

namespace BookRecords.Services
{

    public class AuthorService : IAuthorService
    {
        private readonly bookrecordsContext _context;

        public AuthorService(bookrecordsContext context)
        {
            _context = context;
        }

        public async Task<GetAuthorsResponse> GetAuthorsAsync()
        {
            var authors = await _context.Authors.ToListAsync();

            if (authors.Count == 0)
            {
                return new GetAuthorsResponse
                {
                    Success = false,
                    Error = "No Authors found",
                    ErrorCode = "A04"
                };
            }
            return new GetAuthorsResponse { Success = true, Authors = authors };
        }

        public async Task<AuthorResponse> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author is null)
            {
                return new AuthorResponse { 
                    Success = false, 
                    Error = "Author not found", 
                    ErrorCode= "A02",
                };
            }

            return new AuthorResponse 
            { 
                Success = true, 
                Idauthor=author.Idauthor, 
                Firstname = author.Firstname, 
                Lastname = author.Lastname 
            };
        }


        public async Task<AuthorResponse> UpdateAuthorAsync(int id, Author author)
        {
            if (id != author.Idauthor)
            {
                return new AuthorResponse
                {
                    Success=false,
                    Error = "Bad Request",
                    ErrorCode = ""

                };
            }

            //if (author is null)
            //{
            //    return new AuthorResponse {
            //        Success = false,
            //        Error = "Author Not Found.",
            //        ErrorCode = "A02",
            //    };
            //}

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return new AuthorResponse
                    {
                        Success = false,
                        Error = "Author Not found",
                        ErrorCode = "A02",

                    };
                }
                else
                {
                    throw;
                }
            }

            return new AuthorResponse {
                Success = true,
                Idauthor = author.Idauthor,
                Firstname = author.Firstname,
                Lastname = author.Lastname
            };
        }

        public async Task<AuthorResponse> DeleteAuthorkAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return new AuthorResponse
                {
                    Success = false,
                    Error = "Author Not Found",
                    ErrorCode = "A02"
                };
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return new AuthorResponse
            {
                Success = true,
                Idauthor = author.Idauthor,
                
            };
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Idauthor == id);
        }
    }
}
//ERROR CODES USED:
//A02: Author not found
//A01:
//A03
//A04: No Authors Found