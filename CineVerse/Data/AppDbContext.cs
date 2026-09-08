using CineVerse.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace CineVerse.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Movie>movies { get; set; }

        public DbSet<Genre>Genres { get; set; }


        public DbSet<SearchHistory> searchHistories { get; set; }



    }
}
