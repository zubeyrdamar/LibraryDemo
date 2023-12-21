using Library.DataAccess.Abstract;
using Library.Entity;

namespace Library.DataAccess.Concrete.Sql
{
    public class SqlBorrowingRepository : GenericRepository<Borrowing, LibraryDbContext>, IBorrowingRepository
    {

    }
}
