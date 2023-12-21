using Library.DataAccess.Abstract;
using Library.Entity;

namespace Library.DataAccess.Concrete.Sql
{
    public class SqlBookRepository : GenericRepository<Book, LibraryDbContext>, IBookRepository
    {

    }
}
