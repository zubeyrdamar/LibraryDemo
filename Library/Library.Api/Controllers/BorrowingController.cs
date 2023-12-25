using AutoMapper;
using Library.Api.Models.Borrowing;
using Library.Business.Abstract;
using Library.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly ILogger<BorrowingController> logger;
        private readonly IMapper mapper;
        private readonly IBorrowingService service;
        private readonly IBookService bookService;

        public BorrowingController(ILogger<BorrowingController> logger, IMapper mapper, IBorrowingService service, IBookService bookService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.service = service;
            this.bookService = bookService;
        }

        [HttpPost]
        [Route("borrow")]
        [Authorize]
        public IActionResult Borrow(BorrowDTO borrowDTO) 
        {
            try
            {
                // validate request body
                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Validation error while borrowing a book");
                    throw new Exception("Validation error while borrowing a book");
                }

                // convert dto to entity
                var borrow = mapper.Map<Borrowing>(borrowDTO);

                // get book to check if available to be borrowed
                var book = bookService.Read(borrow.BookId);
                if (book == null || book.IsBorrowed == true)
                {
                    logger.LogWarning("Book is not available to be borrowed.");
                    throw new Exception("Book is not available to be borrowed.");
                }

                // set and save data
                borrow.BorrowingDate = DateTime.Now;
                borrow.ReturningDate = DateTime.Now.AddDays(borrow.Duration);
                borrow.UpdatedAt = DateTime.Now;
                borrow.CreatedAt = DateTime.Now;
                service.Create(borrow);

                // sign book as borrowed
                book.IsBorrowed = true;
                book.UpdatedAt = DateTime.Now;
                bookService.Update(book);

                // save log message
                logger.LogInformation("Book has been borrowed");

                return Ok();
            }
            catch (Exception ex)
            {
                // throw global exception
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("return")]
        [Authorize]
        public IActionResult Return(ReturnDTO returnDTO)
        {
            try
            {
                // validate request body
                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Validation error while returning a book");
                    throw new Exception("Validation error while returning a book");
                }

                // get book to check if available to be returned
                var book = bookService.Read(returnDTO.BookId);
                if (book == null || book.IsBorrowed == false)
                {
                    logger.LogInformation("Book was not available to be returned.");
                    throw new Exception("Book was not available to be returned.");
                }

                // get borrowing data
                var borrow = service.Find(returnDTO.BookId);

                // In this project we destroy data from database. It is also possible to delete safely.
                service.Delete(borrow);

                // sign book as returned
                book.IsBorrowed = false;
                book.UpdatedAt = DateTime.Now;
                bookService.Update(book);

                // save log message
                logger.LogInformation("Book has been returned");

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
