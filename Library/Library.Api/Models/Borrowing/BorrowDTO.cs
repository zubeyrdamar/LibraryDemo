﻿using System.ComponentModel.DataAnnotations;

namespace Library.Api.Models.Borrowing
{
    public class BorrowDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
