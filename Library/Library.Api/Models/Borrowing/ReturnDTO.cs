using System.ComponentModel.DataAnnotations;

namespace Library.Api.Models.Borrowing
{
    public class ReturnDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid BookId { get; set; }
    }
}
