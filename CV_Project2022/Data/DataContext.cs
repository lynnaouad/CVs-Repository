using Microsoft.EntityFrameworkCore;

namespace CV_Project2022.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite Primary Key
            modelBuilder.Entity<CV_SKILL>()
                .HasKey(k => new { k.CvId, k.SkillId });

        }

        public DbSet<CV> CV { get; set; }
        public DbSet<SKILL> SKILL { get; set; }
        public DbSet<CV_SKILL> CV_SKILL { get; set; }
    }
}
