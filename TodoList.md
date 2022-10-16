# Todo list
Tasks to do:
## Important => To get API working with basic functionalities

- User CRUD [DONE]
- Book CRUD [DONE]
- Author CRUD [DONE]
- Categories CRUD [DONE]

- Attach / Detach Book to User [DONE]
- Attach / Detach Author to book [DONE]
- Attach / Detach Category to Book [DONE]

- Get user's books (Books that are owned by the user) => [DONE], Shows User, book, book's author and book's categories
- Get author's books (Books that author has written)[DONE]
- Get book's categories (Categories that book belongs to) [DONE], GetBookById shows book's authors, categories
- Simple username & password login [DONE]

- Login method (JWT Accesstoken + Refreshtoken) [DONE]
- Logout method => revoke JWT token [DONE]
- Moved Sensitive information to User secrets file that is stored elsewhere. (Secrets.json) [DONE]

## Medium => if there is time
- JWT token creation & validation (Belongs to login & logout) [DONE]
- Input validation
- Null value validation & Handling & checking. => DATABASE null value handling
- Possible roles (User, Admin, etc.)
- Double value checking when creating Database objects => no double values in DB
- Data validation => Are all parameters valid etc.
- Use of DTOs (Data Transfer Objects)
- API security => what belongs under this category?
- Possible image upload for profile?
- User's profile data


## Basic stuff need to be done
 - Update README
 - Create Frontend
 - DATETIME formats & formatting
 - ADD [Authorize] to Controllers => this adds Authorize middleware to controllers [DONE]
 - Check how to store SENSITIVE information => Connection strings, Jwt secrets etc. [DONE]