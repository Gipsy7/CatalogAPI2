﻿using CatalogAPI2.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI2.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
    }
}