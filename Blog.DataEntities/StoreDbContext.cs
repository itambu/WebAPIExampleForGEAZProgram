using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataEntities
{
    public class StoreDbContext : DbContext
    {
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();   // create database on first request
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var temp = new CategoryEntity[] 
            {
                new CategoryEntity { Id = 1, Name = "Tools" },
                new CategoryEntity { Id = 2, Name = "Food" }
            };

            modelBuilder.Entity<CategoryEntity>().HasData(temp
            );

            //modelBuilder.Entity<ItemEntity>()
            //    .HasData(
            //        new ItemEntity { Id = 1, Name = "Nail", Category = temp[0], Description = "strong" },
            //        new ItemEntity { Id = 2, Name = "Hammer", Category = temp[0], Description = "heavy" },
            //        new ItemEntity { Id = 3, Name = "Glue", Category = temp[0], Description = "stronger than a nail" }
            //);
        }
    }
}
