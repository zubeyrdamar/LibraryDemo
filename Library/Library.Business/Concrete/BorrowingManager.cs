using Library.Business.Abstract;
using Library.DataAccess.Abstract;
using Library.Entity;

namespace Library.Business.Concrete
{
    public class BorrowingManager : IBorrowingService
    {
        private readonly IBorrowingRepository repository;
        public BorrowingManager(IBorrowingRepository repository)
        {
            this.repository = repository;
        }

        public List<Borrowing> List()
        {
            return repository.List().ToList();
        }

        public Borrowing Read(Guid Id)
        {
            return repository.Read(Id);
        }

        public void Create(Borrowing Borrowing)
        {
            repository.Create(Borrowing);
        }

        public void Update(Borrowing Borrowing)
        {
            repository.Update(Borrowing);
        }

        public void Delete(Borrowing Borrowing)
        {
            repository.Delete(Borrowing);
        }
    }
}
