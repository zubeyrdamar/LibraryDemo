using Library.Entity;

namespace Library.DataAccess.Abstract
{
    public interface IBorrowingRepository : IRepository<Borrowing>
    {
        public Borrowing Find(Guid BookId);
    }
}
