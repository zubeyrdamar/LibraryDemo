using Library.DataAccess.Abstract;
using Library.Entity;

namespace Library.DataAccess.Concrete.Sql
{
    public class SqlBorrowingRepository : GenericRepository<Borrowing, LibraryDbContext>, IBorrowingRepository
    {
        public Borrowing Find(Guid BookId)
        {
            using (var context = new LibraryDbContext())
            {
                return context.Borrowing.Where(b => b.BookId == BookId).First();
            }
        }
    }
}
