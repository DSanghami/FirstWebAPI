using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPI.Model;

namespace MyFirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        static List<Book> bookList = new List<Book>();
        [HttpGet]
        public List<Book> GetBooks()
        {
           
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book();
                book.BookID = i;
                book.Title = "Atomic habits" + i;
                book.Cost = i * 100;
                book.AuthorName = "Sanghami" + i;
                bookList.Add(book);
            }
            return bookList;
        }
        [HttpPost]
        public int AddBook(Book book)
        {
            bookList.Add(book);
            return 1;
        }
    }
}
