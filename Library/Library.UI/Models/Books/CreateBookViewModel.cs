using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.Books
{
    public class CreateBookViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
