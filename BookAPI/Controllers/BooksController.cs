using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private static List<Book> _books = new List<Book>
    {
        new Book { Title = "Book One", Author = "Author One", Genre = "Fiction", PublishedYear = 2001 },
        new Book { Title = "Book Two", Author = "Author Two", Genre = "Non-fiction", PublishedYear = 2005 },
    };

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(_books);
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound(new { message = "Book not found" });
        }
        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> AddBook([FromBody] Book newBook)
    {
        if (string.IsNullOrWhiteSpace(newBook.Title) ||
            string.IsNullOrWhiteSpace(newBook.Author) ||
            string.IsNullOrWhiteSpace(newBook.Genre) ||
            newBook.PublishedYear <= 0)
        {
            return BadRequest(new { message = "Invalid book data. Make sure all fields are valid." });
        }

        var book = new Book
        {
            Title = newBook.Title,
            Author = newBook.Author,
            Genre = newBook.Genre,
            PublishedYear = newBook.PublishedYear
        };
        _books.Add(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound(new { message = "Book not found" });
        }

        if (string.IsNullOrWhiteSpace(updatedBook.Title) ||
            string.IsNullOrWhiteSpace(updatedBook.Author) ||
            string.IsNullOrWhiteSpace(updatedBook.Genre) ||
            updatedBook.PublishedYear <= 0)
        {
            return BadRequest(new { message = "Invalid book data. Make sure all fields are valid." });
        }

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.Genre = updatedBook.Genre;
        book.PublishedYear = updatedBook.PublishedYear;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound(new { message = "Book not found" });
        }

        _books.Remove(book);
        return NoContent();
    }
}
