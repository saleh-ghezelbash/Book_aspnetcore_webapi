﻿using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController: ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody]BookVM bookVM)
        {
            _booksService.AddBookWithAuthors(bookVM);
            return Ok();
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            return Ok(_booksService.GetAllBooks());
        }

        [HttpGet("get-book-with-authors-by-id/{id}")]
        public IActionResult GetBookWithAuthorsById(int id)
        {
            return Ok(_booksService.GetBookById(id));
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id,[FromBody] BookVM bookVM)
        {
            return Ok(_booksService.UpdateBookById(id,bookVM));
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id)
        {
            _booksService.DeleteBookById(id);
            return Ok();
        }
    }
}
