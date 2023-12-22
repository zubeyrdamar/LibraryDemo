using Library.Entity;

namespace Library.DataAccess.Abstract
{
    public interface IBookRepository : IRepository<Book>
    {
        public Book Details(Guid id);
    }
}
