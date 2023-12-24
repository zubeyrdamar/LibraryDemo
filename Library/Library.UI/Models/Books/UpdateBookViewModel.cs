using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.Books
{
    public class UpdateBookViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile Image { get; set; }
    }
}
