﻿using IS_Projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace IS_Projekt.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<InternetUse> InternetUseData { get; set; }
        public DbSet<ECommerce> ECommerceData { get; set; }
        // tutaj pozniej inne dbset dla danych

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // For example, to add a unique constraint on the Username property of User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }

    }
}
