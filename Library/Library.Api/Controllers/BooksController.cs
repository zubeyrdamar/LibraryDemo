using AutoMapper;
using Library.Api.Models.Book;
using Library.Business.Abstract;
using Library.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBookService service;
        private readonly UserManager<IdentityUser> userManager;

        public BooksController(IMapper mapper, IBookService service, UserManager<IdentityUser> userManager)
        {
            this.mapper = mapper;
            this.service = service;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult List() 
        {
            var books = service.List();
            var booksDTO = mapper.Map<List<BookDTO>>(books);
            return Ok(booksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var book = service.Details(id);
            if (book == null) { return NotFound(); }

            var user = new IdentityUser();
            if(book.Borrowing != null)
            {
                 user = await userManager.FindByIdAsync(book.Borrowing.UserId.ToString());
            }

            var bookDTO = new BookDetailsDTO
            {
                Id = id,
                Name = book.Name,
                Description = book.Description,
                Author = book.Author,
                ImageUrl = book.ImageUrl,
                IsBorrowed = book.IsBorrowed,
                User = user
            };

            return Ok(bookDTO);
        }

        [HttpPost]
        public IActionResult Create(AddBookDTO bookDTO)
        {
            if (bookDTO == null) { return BadRequest(); }
            var book = mapper.Map<Book>(bookDTO);
            book.IsBorrowed = false;
            book.UpdatedAt = DateTime.Now;
            book.CreatedAt = DateTime.Now;
            service.Create(book);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Book book)
        {
            var tempBook = service.Read(book.Id);
            if (tempBook == null) { return BadRequest(); }

            tempBook.Name = book.Name;
            tempBook.Description = book.Description;
            tempBook.Author = book.Author;
            tempBook.ImageUrl = book.ImageUrl;
            tempBook.UpdatedAt = DateTime.Now;
            service.Update(tempBook);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var book = service.Read(id);
            if (book == null) { return BadRequest(); }

            service.Delete(book);
            return Ok();
        }
    }
}
