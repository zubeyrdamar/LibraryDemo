using System.ComponentModel.DataAnnotations;

namespace Library.Api.Models.Book
{
    public class AddBookDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
