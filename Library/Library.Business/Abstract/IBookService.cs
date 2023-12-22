using Library.Entity;

namespace Library.Business.Abstract
{
    public interface IBookService
    {
        List<Book> List();
        Book Read(Guid id);
        Book Details(Guid id);
        void Create(Book Book);
        void Update(Book Book);
        void Delete(Book Book);
    }
}
