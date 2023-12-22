using AutoMapper;
using Library.Api.Models.Borrowing;
using Library.Business.Abstract;
using Library.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBorrowingService service;
        private readonly IBookService bookService;

        public BorrowingController(IMapper mapper, IBorrowingService service, IBookService bookService)
        {
            this.mapper = mapper;
            this.service = service;
            this.bookService = bookService;
        }

        [HttpPost]
        [Route("borrow")]
        public IActionResult Borrow(BorrowDTO borrowDTO) 
        {
            if(!ModelState.IsValid) { return BadRequest(); }
            
            var borrow = mapper.Map<Borrowing>(borrowDTO);
            var book = bookService.Read(borrow.BookId);

            if (book == null || book.IsBorrowed == true)
            {
                return BadRequest("Book is not available to be borrowed.");
            }

            borrow.BorrowingDate = DateTime.Now;
            borrow.ReturningDate = DateTime.Now.AddDays(borrow.Duration);
            borrow.UpdatedAt = DateTime.Now;
            borrow.CreatedAt = DateTime.Now;
            service.Create(borrow);

            // sign book as borrowed
            book.IsBorrowed = true;
            book.UpdatedAt = DateTime.Now;
            bookService.Update(book);

            return Ok();
        }

        [HttpPost]
        [Route("return")]
        public IActionResult Return(ReturnDTO returnDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var book = bookService.Read(returnDTO.BookId);
            if (book == null || book.IsBorrowed == false)
            {
                return BadRequest("Book is not available to be returned.");
            }

            var borrow = service.Find(returnDTO.BookId);
            service.Delete(borrow);

            // sign book as returned
            book.IsBorrowed = false;
            book.UpdatedAt = DateTime.Now;
            bookService.Update(book);

            return Ok();
        }
    }
}
