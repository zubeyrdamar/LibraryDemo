using Library.Entity;

namespace Library.Business.Abstract
{
    public interface IBorrowingService
    {
        List<Borrowing> List();
        Borrowing Read(Guid Id);
        Borrowing Find(Guid BookId);
        void Create(Borrowing Borrowing);
        void Update(Borrowing Borrowing);
        void Delete(Borrowing Borrowing);
    }
}
