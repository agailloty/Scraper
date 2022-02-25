using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = "Data Source=jobs.db";
            optionsBuilder.UseSqlite(connString);
        }
        public DbSet<JobOffer> Jobs { get; set; }
    }
}