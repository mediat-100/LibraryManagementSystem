using LMS.Application.Contracts.IService;
using LMS.Application.Dtos.Requests;
using LMS.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Roles = "User")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequestDto request)
        {
            var response = await _bookService.AddBook(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBook(long id)
        {
            var response = await _bookService.GetBook(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBook(long id, UpdateBookRequestDto request)
        {
            var response = await _bookService.UpdateBook(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            var response = await _bookService.DeleteBook(id);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult SearchBooks(string? title, string? author, string? isbn, DateTime? publishedDate, int page = 1, int pageSize = 10)
        {
            var response = _bookService.SearchBooks(title, author, isbn, publishedDate, page, pageSize);
            return Ok(response);
        }
    }
}
