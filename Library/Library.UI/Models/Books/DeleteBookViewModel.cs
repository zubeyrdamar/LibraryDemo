using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.Books
{
    public class DeleteBookViewModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
