using System.ComponentModel.DataAnnotations;

namespace Library.Entity
{
    public class Borrowing : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int Duration { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime ReturningDate { get; set; }

        public Book Book { get; set; }
    }
}
