using Microsoft.EntityFrameworkCore;

namespace Recipe.DataAcess.Models
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext()
        {
        }

        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
           : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Step> Steps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conStr = "server=LAPTOP-0LAB0G9K\\TXB_SQLLAP;database=RecipeDb;user=sa;password=123;TrustServerCertificate=true";
                optionsBuilder.UseSqlServer(conStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Reviews)
                .WithOne(rv => rv.Recipe)
                .HasForeignKey(rv => rv.RecipeId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Steps)
                .WithOne(s => s.Recipe)
                .HasForeignKey(s => s.RecipeId);

            modelBuilder.Entity<Review>()
                .HasKey(rv => rv.ReviewId);

            modelBuilder.Entity<Ingredient>()
                .HasKey(i => i.IngredientId);

            modelBuilder.Entity<Step>()
                .HasKey(s => s.StepId);
        }
    }
}
