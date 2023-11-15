﻿using Microsoft.Extensions.Hosting;

namespace PosterMaker.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public int AppId { get; set; }
        public App App{ get; set; } = null;
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
