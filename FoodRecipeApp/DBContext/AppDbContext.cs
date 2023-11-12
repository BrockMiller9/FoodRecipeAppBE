using FoodRecipeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FoodRecipeApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<UserFavoriteRecipe> UserFavoriteRecipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFavoriteRecipe>()
                .HasKey(ufr => new { ufr.UserId, ufr.RecipeId });

            modelBuilder.Entity<UserFavoriteRecipe>()
                .HasOne(ufr => ufr.User)
                .WithMany(u => u.UserFavoriteRecipes)
                .HasForeignKey(ufr => ufr.UserId);

            modelBuilder.Entity<UserFavoriteRecipe>()
                .HasOne(ufr => ufr.Recipe)
                .WithMany(r => r.UserFavoriteRecipes)
                .HasForeignKey(ufr => ufr.RecipeId);
        }
    }

}
