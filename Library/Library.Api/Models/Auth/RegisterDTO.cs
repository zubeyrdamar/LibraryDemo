using System.ComponentModel.DataAnnotations;

namespace Library.Api.Models.Auth
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(30)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
