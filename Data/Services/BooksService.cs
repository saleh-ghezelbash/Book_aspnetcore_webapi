using System;
using System.Collections.Generic;
using System.Linq;
using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBookWithAuthors(BookVM bookModel)
        {
            var book = new Book() {
                CoverUrl = bookModel.CoverUrl,
                Description = bookModel.Description,
                Genre = bookModel.Genre,
                IsRead = bookModel.IsRead,
                Rate = bookModel.IsRead ? bookModel.Rate.Value : null,
                Title = bookModel.Title,
                DateAdded = DateTime.Now,
                DateRead = bookModel.IsRead ? bookModel.DateRead.Value : null,
                PublisherId = bookModel.PublisherId
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            foreach (var id in bookModel.AuthorIds)
            {
                var book_author = new Book_Author()
                {
                    AuthorId = id,
                    BookId = book.Id
                };
                _context.Book_Authors.Add(book_author);
                _context.SaveChanges();
            }
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var bookWithAuthors = _context.Books.Where(w => w.Id == bookId).Select(b => new BookWithAuthorsVM()
            {
                CoverUrl = b.CoverUrl,
                Description = b.Description,
                Genre = b.Genre,
                IsRead = b.IsRead,
                Rate = b.IsRead ? b.Rate.Value : null,
                Title = b.Title,
                DateRead = b.IsRead ? b.DateRead.Value : null,
                PublisherName = b.Publisher.Name,
                AuthorNames = b.Book_Authors.Select(s=>s.Author.FullName).ToList()

            }).FirstOrDefault();
            return bookWithAuthors;
        }

        public Book UpdateBookById(int bookId,BookVM bookModel)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.CoverUrl = bookModel.CoverUrl;
                book.Description = bookModel.Description;
                book.Genre = bookModel.Genre;
                book.IsRead = bookModel.IsRead;
                book.Rate = bookModel.IsRead ? bookModel.Rate.Value : null;
                book.Title = bookModel.Title;
                book.DateRead = bookModel.IsRead ? bookModel.DateRead.Value : null;

                _context.SaveChanges();
            }
            return book;
        }

        public void DeleteBookById(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
