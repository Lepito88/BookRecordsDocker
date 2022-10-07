SELECT * FROM bookrecords.user;

# Get user's books (delete user.username)
SELECT user.username, book.book_name as 'Bookname', book.type, concat(author.firstname,' ',author.lastname) as 'Author', category.category_name as 'Category' FROM user
 INNER JOIN user_books ON user_books.iduser = user.iduser 
 INNER JOIN book on user_books.idbook = book.idbook
 
 INNER JOIN author_books on author_books.idbook = book.idbook
 INNER JOIN author on author.idauthor = author_books.idauthor
 
 INNER JOIN book_categories on book_categories.idbook = book.idbook
 INNER JOIN category on book_categories.idcategory = category.idcategory
 
 #INNER JOIN author_books on book.idbook = author_books.idbook
 #INNER JOIN author on author_books.idbook = book.idbook
 where user.iduser = 1
