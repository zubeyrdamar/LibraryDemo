using AutoMapper;
using Library.Api.Models.Book;
using Library.Business.Abstract;
using Library.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> logger;
        private readonly IMapper mapper;
        private readonly IBookService service;
        private readonly UserManager<IdentityUser> userManager;

        public BooksController(ILogger<BooksController> logger, IMapper mapper, IBookService service, UserManager<IdentityUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.service = service;
            this.userManager = userManager;
        }


        [HttpGet]
        [Authorize]
        public IActionResult List() 
        {
            try
            {
                // get all books
                var books = service.List();

                // convert books to dto
                var booksDTO = mapper.Map<List<BookDTO>>(books);

                // save log message
                logger.LogInformation("Books has been listed");

                return Ok(booksDTO);
            }
            catch (Exception ex)
            {
                // throw global exception
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                // get book's details
                var book = service.Details(id);

                // throw error if book does not exist
                if (book == null)
                {
                    logger.LogWarning("Book with given id is not found while reading a book's details.");
                    return NotFound("Book with given id is not found while reading a book's details.");
                }

                // get user who borrowed the book
                var user = new IdentityUser(); // default user to escape null errors
                if (book.Borrowing != null)
                {
                    user = await userManager.FindByIdAsync(book.Borrowing.UserId.ToString());
                    if(user == null)
                    {
                        return NotFound("User does not exist");
                    }
                }

                // convert book to dto
                var bookDTO = new BookDetailsDTO
                {
                    Id = id,
                    Name = book.Name,
                    Description = book.Description,
                    Author = book.Author,
                    ImageUrl = book.ImageUrl,
                    IsBorrowed = book.IsBorrowed,
                    Borrowing = book.Borrowing,
                    User = user,
                };

                // save log message
                logger.LogInformation("Book details has been shown");

                return Ok(bookDTO);
            }
            catch (Exception ex)
            {
                // throw global exception
                throw new Exception(ex.Message);
            }

        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(AddBookDTO bookDTO)
        {
            try
            {
                // validate body
                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Validation error occured while creating a book.");
                    return BadRequest("Validation error occured while creating a book.");
                }

                // set and save data
                var book = mapper.Map<Book>(bookDTO); // convert dto to book
                book.IsBorrowed = false;
                book.UpdatedAt = DateTime.Now;
                book.CreatedAt = DateTime.Now;
                service.Create(book);

                // save log message
                logger.LogInformation("Book has been created");

                return Ok();
            }
            catch (Exception ex)
            {
                // throw global exception
                throw new Exception(ex.Message);
            }
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(BookDTO bookDTO)
        {
            try
            {
                // validate body
                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Validation error occured while updating a book.");
                    return BadRequest("Validation error occured while updating a book.");
                }

                // read the book
                var book = mapper.Map<Book>(bookDTO);
                var tempBook = service.Read(book.Id);

                // throw error if the book does not exist
                if (tempBook == null)
                {
                    logger.LogWarning("Book with given id is not found while updating a book.");
                    throw new Exception("Book with given id is not found while updating a book.");
                }

                // set and save data
                tempBook.Name = book.Name;
                tempBook.Description = book.Description;
                tempBook.Author = book.Author;
                tempBook.ImageUrl = book.ImageUrl;
                tempBook.UpdatedAt = DateTime.Now;
                service.Update(tempBook);

                // save log message
                logger.LogInformation("Book has been updated");

                return Ok();
            }
            catch (Exception ex)
            {
                // throw global exception
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                // read the book
                var book = service.Read(id);

                // throw error if the book does not exist
                if (book == null)
                {
                    logger.LogWarning("Book with given id is not found while deleting a book.");
                    throw new Exception("Book with given id is not found while deleting a book.");
                }

                // throw error if the book is borrowed
                // deleting a book is borrowed may cause errors. First, make sure it is returned.
                if (book.IsBorrowed == true)
                {
                    logger.LogCritical("Borrowed book cannot be deleted.");
                    throw new Exception("Borrowed book cannot be deleted.");
                }

                // delete the book
                service.Delete(book);

                // save log message
                logger.LogInformation("Book has been deleted");

                return Ok();
            }
            catch (Exception ex)
            {
                // throw global exception
                throw new Exception(ex.Message);
            }
        }

    }
}
