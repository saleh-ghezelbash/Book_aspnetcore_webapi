using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books.ActionResults;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController: ControllerBase
    {
        private readonly PublisherService _publisherService;
        private readonly ILogger<PublisherController> _logger;

        public PublisherController(PublisherService publisherService, ILogger<PublisherController> logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisherVm)
        {
            try
            {
            var publisher = _publisherService.AddPublisher(publisherVm);
            return Created(nameof(AddPublisher), publisher);

            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
           // throw new Exception("my custom error");
            try
            {
                _logger.LogInformation("This is a log in GetAllPublishers()");
                var _result = _publisherService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(_result);
            }
            catch (Exception)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public ActionResult<Publisher> GetPublisherById(int id)
        {
            var _response = _publisherService.GetPublisherById(id);

            if (_response != null)
            {
                //var _responseObj = new CustomActionResultVM()
                //{
                //    Publisher = _response
                //};
                //return new CustomActionResult(_responseObj);

                return _response;
                //return Ok(_response);
            }
            else
            {
                //var _responseObj = new CustomActionResultVM()
                //{
                //    Exception = new Exception("This is comming from Publisher controller!")
                //};
                //return new CustomActionResult(_responseObj);

                return NotFound();
            }
        }

        [HttpGet("get-publisher-with-books-and-authors-by-id/{id}")]
        public IActionResult GetPublisherWithBooksAndAuthorsById(int id)
        {
            return Ok(_publisherService.GetPublisherWithBooksAndAuthorsById(id));
        }

        [HttpPut("update-publisher-by-id/{id}")]
        public IActionResult UpdatePublisherById(int id, [FromBody] PublisherVM publisherVm)
        {
            return Ok(_publisherService.UpdateBookById(id, publisherVm));
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
            _publisherService.DeletePublisherById(id);
            return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
