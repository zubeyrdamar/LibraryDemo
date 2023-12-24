using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.Borrowing
{
    public class BorrowViewModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
