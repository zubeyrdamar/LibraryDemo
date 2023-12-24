using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.Borrowing
{
    public class ReturnViewModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid BookId { get; set; }
    }
}
