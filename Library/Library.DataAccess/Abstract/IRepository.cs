namespace Library.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        T Read(Guid id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
