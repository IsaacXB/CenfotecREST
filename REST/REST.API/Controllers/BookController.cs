using Microsoft.AspNetCore.Mvc;
using REST.API.Models;
using System.Xml.Linq;

namespace REST.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private List<Book> _books = new List<Book>()
        {
            new Book {
                    Id = 1,
                    Name = "Harry Potter and the Sorcerer's Stone, Book 1",
                    Author = "J.K. Rowling",
                    Description = "Harry Potter has never even heard of Hogwarts when the letters start dropping on the doormat at number four, Privet Drive."
                },
            new Book {
                    Id = 2,
                    Name = "The Fellowship of the Ring: Book One in The Lord of the Rings Trilogy",
                    Author = "J. R. R. Tolkien",
                    Description = "The Fellowship of the Ring, the first volume in the trilogy, tells of the fateful power of the One Ring."
                },
        };
        [HttpGet("GetBooks")]
        public ActionResult<List<Book>> GetBooks()
        {
            if (_books != null && _books.Count > 0)
            {
                return Ok(_books);
            }
            return NotFound();

        }

        [HttpGet("GetBook/{id:int}")]
        public IActionResult GetBook(int id)
        {
            Book? book = _books.FirstOrDefault(x => x.Id.Equals(id));
            if (book != null)
            {
                return Ok(book);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            book.Id = _books.Max(x => x.Id) + 1;
            _books.Add(book);
            return Ok(book);

        }

        [HttpPut("{id:int}")]
        public ActionResult<Book> Put(int id, [FromBody] Book book)
        {
            Book? currentBook = _books.FirstOrDefault(x => x.Id.Equals(id));
            if (currentBook != null)
            {
                currentBook.Name = book.Name;
                currentBook.Author = book.Author;
                currentBook.Description = book.Description;

                return Ok(currentBook);

            }
            return NotFound();

        }

        [HttpDelete("{id:int}")]
        public ActionResult<Book> Delete(int id)
        {
            Book? bookToDelete = _books.FirstOrDefault(x => x.Id.Equals(id));
            if (bookToDelete != null)
            {
                _books.Remove(bookToDelete);

                return Ok(new Book());

            }
            return NotFound();

        }
    }
}
