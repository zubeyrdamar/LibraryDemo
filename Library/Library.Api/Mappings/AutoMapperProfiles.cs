using AutoMapper;
using Library.Api.Models.Book;
using Library.Api.Models.Borrowing;
using Library.Entity;

namespace Library.Api.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, BookDetailsDTO>().ReverseMap();
            CreateMap<Book, AddBookDTO>().ReverseMap();

            CreateMap<Borrowing, BorrowDTO>().ReverseMap();
        }
    }
}
