using Library.Business.Abstract;
using Library.DataAccess.Abstract;
using Library.Entity;

namespace Library.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookRepository repository;
        public BookManager(IBookRepository repository)
        {
            this.repository = repository;
        }

        public List<Book> List()
        {
            return repository.List().OrderBy(o => o.Name).ToList();
        }

        public Book Read(Guid Id)
        {
            return repository.Read(Id);
        }

        public void Create(Book Book)
        {
            repository.Create(Book);
        }

        public void Update(Book Book)
        {
            repository.Update(Book);
        }

        public void Delete(Guid Id)
        {
            repository.Delete(Id);
        }
    }
}
