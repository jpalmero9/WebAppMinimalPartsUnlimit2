﻿namespace WebAppMinimalPartsUnlimit2.Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
