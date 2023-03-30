using Microsoft.EntityFrameworkCore;

namespace API.Entities
{

    public class MeetUpContext : DbContext
    {
        public string _connectionstring = "Server=(localdb)\\mssqllocaldb;Database=MeetUpDb;Trusted_Connection=True;";
        public DbSet<MeetUp> MeetUps { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetUp>()
                .HasOne(m => m.Location)
                .WithOne(l => l.MeetUp)
                .HasForeignKey<Location>(l => l.MeetUpId);

            modelBuilder.Entity<MeetUp>()
                .HasMany(m => m.Lectures)
                .WithOne(l => l.MeetUp);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionstring);

        }
    }
}
