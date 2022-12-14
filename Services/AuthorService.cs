using BookRecords.Data;
using BookRecords.Data.Entities;
using BookRecords.Interfaces;
using BookRecords.Responses;
using BookRecords.Responses.Authors;
using BookRecords.Responses.Books;
using Microsoft.EntityFrameworkCore;

namespace BookRecords.Services
{

    public class AuthorService : IAuthorService
    {
        private readonly BookRecordsContext _context;

        public AuthorService(BookRecordsContext context)
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
            var author = await _context.Authors
                .Where(author => author.Idauthor == id)
                .Include(_=> _.Books)
                .ThenInclude(_ => _.Categories)
                .FirstOrDefaultAsync();
            if (author is null)
            {
                return new AuthorResponse { 
                    Success = false, 
                    Error = "Author not found", 
                    ErrorCode= "A02",
                };
            }

            var abooks = author.Books.ToList();
            var tmpList = new List<BooksResponseForAuthor>();
            foreach (var book in abooks)
            {
                tmpList.Add(new BooksResponseForAuthor
                {
                    BookId = book.Idbook,
                    BookName = book.BookName,
                    Type = book.Type,
                    Isbn = book.Isbn,
                    ReleaseYear = (DateTime)book.ReleaseYear,
                    Categories = book.Categories,
                });

            }

            return new AuthorResponse 
            { 
                Success = true, 
                Idauthor=author.Idauthor, 
                Firstname = author.Firstname, 
                Lastname = author.Lastname,
                Books = tmpList
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

        public async Task<AuthorResponse> DeleteAuthorAsync(int id)
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
            var deleteResponse = await _context.SaveChangesAsync();
            if (deleteResponse >= 0)
            {
                return new AuthorResponse
                {
                    Success = true,
                    Idauthor = author.Idauthor
                };
            }

            return new AuthorResponse
            {
                Success = false,
                Error = "Unable to delete author",
                ErrorCode = "A03"
                
            };
        }
        public async Task<AuthorResponse> CreateAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);

            var createResponse = await _context.SaveChangesAsync();

            if (createResponse >= 0)
            {
                return new AuthorResponse {
                    Success = true,
                    Idauthor=author.Idauthor,
                    Firstname=author.Firstname,
                    Lastname=author.Lastname,
                };
            }
            return new AuthorResponse
            {
                Success = false,
                Error = "Unable to save author",
                ErrorCode = "A05"
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
//A03: Unable to Delete author
//A04: No Authors Found
//A05 : Unable to save author