using AutoMapper;
using Library.Api.Models.Borrowing;
using Library.Business.Abstract;
using Library.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBorrowingService service;

        public BorrowingController(IMapper mapper, IBorrowingService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpPost]
        [Route("borrow")]
        public IActionResult Borrow(BorrowDTO borrowDTO) 
        {
            if(!ModelState.IsValid) { return BadRequest(); }
            
            var borrow = mapper.Map<Borrowing>(borrowDTO);
            service.Create(borrow);

            return Ok();
        }

        [HttpPost]
        [Route("return")]
        public async Task<IActionResult> Return()
        {
            return Ok();
        }
    }
}
