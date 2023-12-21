using Library.Entity;

namespace Library.Business.Abstract
{
    public interface IBorrowingService
    {
        List<Borrowing> List();
        Borrowing Read(Guid id);
        void Create(Borrowing Borrowing);
        void Update(Borrowing Borrowing);
        void Delete(Borrowing Borrowing);
    }
}
