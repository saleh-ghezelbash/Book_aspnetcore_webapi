using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using my_books.Data.Models;
using my_books.Data.Paging;
using my_books.Data.ViewModels;
using my_books.Exceptions;

namespace my_books.Data.Services
{
    public class PublisherService
    {
        private AppDbContext _context;
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM model)
        {
            if (StringStartsWithNumber(model.Name)) throw new PublisherNameException("Name starts with number", model.Name);

            var publisher = new Publisher()
            {
                Name = model.Name

            };
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return publisher;
        }

       // public List<Publisher> GetAllPublishers() => _context.Publishers.ToList();

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var allPublishers = _context.Publishers.OrderBy(n => n.Name).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }


            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // Paging
            int pageSize = 5;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);

            return allPublishers;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public PublisherWithBooksAndAuthorsVM GetPublisherWithBooksAndAuthorsById(int publisherId) 
        {
            var data = _context.Publishers.Where(w => w.Id == publisherId).Select(s=>new PublisherWithBooksAndAuthorsVM()
            {
                Name = s.Name,
                BookAuthors = s.Books.Select(ss=>new BookAuthorVM()
                {
                    BookName=ss.Title,
                    BookAuthorNames=ss.Book_Authors.Select(sss=>sss.Author.FullName).ToList()
                }).ToList()

            }).FirstOrDefault();

            return data;
        }

        public Publisher UpdateBookById(int publisherId, PublisherVM publisherModel)
        {
            var publisher = _context.Publishers.FirstOrDefault(b => b.Id == publisherId);
            if (publisher != null)
            {
                publisher.Name = publisherModel.Name;

                _context.SaveChanges();
            }
            return publisher;
        }

        public void DeletePublisherById(int publisherId)
        {
            var publisher = _context.Publishers.FirstOrDefault(b => b.Id == publisherId);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Publisher with id {publisherId} dose not exist!");
            }
        }

        private bool StringStartsWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
    }
}
