using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FoodRecipeApp
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
    }
}
