using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Menu.DataAccess.Models
{
    public class MenuDbContext : DbContext 
    {
        public MenuDbContext()
        {
        }

        public MenuDbContext(DbContextOptions<MenuDbContext> options)
           : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conStr = "server=LAPTOP-0LAB0G9K\\TXB_SQLLAP;database=MenuDb;user=sa;password=123;TrustServerCertificate=true";
                optionsBuilder.UseSqlServer(conStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.Meals)
                .WithOne(m => m.Menu)
                .HasForeignKey(m => m.MenuId);

            modelBuilder.Entity<Meal>()
                .HasMany(m => m.MenuItems)
                .WithOne(mi => mi.Meal)
                .HasForeignKey(mi => mi.MealId);

           
            modelBuilder.Entity<Menu>()
                .Property(m => m.MenuTypes)
                .HasConversion(
                    v => v.ToString(),
                    v => (MenuType)Enum.Parse(typeof(MenuType), v));

            modelBuilder.Entity<Meal>()
                .Property(m => m.MealTypes)
                .HasConversion(
                    v => v.ToString(),
                    v => (MealType)Enum.Parse(typeof(MealType), v));
        }
    }
}
