using System.ComponentModel.DataAnnotations;

namespace Library.Api.Models.Book
{
    public class UpdateBookDTO
    {
        [Required]
        public Guid Id { get; set; }

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
