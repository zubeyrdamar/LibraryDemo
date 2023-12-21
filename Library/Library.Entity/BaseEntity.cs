namespace Library.Entity
{
    public abstract class BaseEntity
    {
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
