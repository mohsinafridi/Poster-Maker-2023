﻿using Microsoft.EntityFrameworkCore;
using PosterMaker.API.Models;

namespace PosterMaker.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<App> Apps { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
