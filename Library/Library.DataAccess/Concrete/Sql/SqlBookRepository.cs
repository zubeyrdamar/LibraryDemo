using Library.DataAccess.Abstract;
using Library.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Concrete.Sql
{
    public class SqlBookRepository : GenericRepository<Book, LibraryDbContext>, IBookRepository
    {
        public Book Details(Guid id)
        {
            using (var context = new LibraryDbContext())
            {
                return context.Books.Where(b => b.Id == id).Include(b => b.Borrowing).First();
            }
        }
    }
}
