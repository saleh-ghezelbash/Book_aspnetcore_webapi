using System.Collections.Generic;
using System.Linq;
using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class AuthorService
    {
        private AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM model)
        {
            var author = new Author()
            {
                FullName = model.FullName

            };
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public List<Author> GetAllAuthors() => _context.Authors.ToList();

        public AuthorWithBookNamesVM GetAuthorById(int authorId)
        {
            var authorWithBooks = _context.Authors.Where(w => w.Id == authorId).Select(s => new AuthorWithBookNamesVM()
            {
                FullName = s.FullName,
                BookTitles = s.Book_Authors.Select(s => s.Book.Title).ToList()
            }).FirstOrDefault();
            return authorWithBooks;
        }

        public Author UpdateBookById(int authorId, AuthorVM authorModel)
        {
            var author = _context.Authors.FirstOrDefault(b => b.Id == authorId);
            if (author != null)
            {
                author.FullName = authorModel.FullName;

                _context.SaveChanges();
            }
            return author;
        }

        public void DeleteAuthorById(int authorId)
        {
            var author = _context.Authors.FirstOrDefault(b => b.Id == authorId);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }
    }
}
