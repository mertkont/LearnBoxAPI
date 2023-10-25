using Business.Contracts;
using DataAccess.DTOs;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BooksController : ControllerBase
{
    private IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }
    
    [HttpGet("GetAllBooks")]
    public ActionResult<IEnumerable<BookDetailsDto>> GetAllBooks()
    {
        var books = _service.GetAllBooks();
        return Ok(books);
    }

    [HttpGet("GetBookById")]
    public ActionResult<BookDetailsDto> GetBookById(int bookId)
    {
        var book = _service.GetBookById(bookId);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }
    
    [HttpGet("GetCompletedBooks")]
    public ActionResult<List<BookDetailsDto>> GetCompletedBooks()
    {
        var books = _service.GetCompletedBooks();
        return Ok(books);
    }
    
    [HttpGet("GetUncompletedBooks")]
    public ActionResult<List<BookDetailsDto>> GetUnCompletedBooks()
    {
        var books = _service.GetUncompletedBooks();
        return Ok(books);
    }

    [HttpPost("AddBook")]
    public ActionResult<Book> CreateBook([FromBody] Book book)
    {
        if (book == null)
        {
            return BadRequest("Unacceptable book data");
        }

        _service.AddBook(book);
        return CreatedAtAction(nameof(GetBookById), new { bookId = book.BookId }, book);
    }

    [HttpPut("UpdateBook")]
    public IActionResult UpdateBook(int bookId, [FromBody] Book book)
    {
        if (book == null || book.BookId != bookId)
        {
            return BadRequest("Unacceptable book data");
        }

        _service.UpdateBook(book);
        return NoContent();
    }

    [HttpDelete("DeleteBook")]
    public IActionResult DeleteBook(int bookId)
    {
        _service.DeleteBook(bookId);
        return NoContent();
    }
}