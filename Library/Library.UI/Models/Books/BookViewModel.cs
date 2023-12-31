﻿using Microsoft.AspNetCore.Identity;

namespace Library.UI.Models.Books
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBorrowed { get; set; }
        
        public Entity.Borrowing Borrowing { get; set; }
        public IdentityUser User { get; set; }
    }
}
