using System.ComponentModel.DataAnnotations;

namespace Library.Entity
{
    public class Book : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBorrowed { get; set; }

        public Borrowing Borrowing { get; set; }
    }
}
