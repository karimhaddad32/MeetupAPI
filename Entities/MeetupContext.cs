using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Entities
{
    public class MeetupContext(DbContextOptions<MeetupContext> options) : DbContext(options)
    {
        public DbSet<Meetup> Meetups { get; set;}
        public DbSet<Location> Locations { get; set;}
        public DbSet<Lecture> Lectures { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Meetup>().HasData(MeetupData.MockMeetUps);
            modelBuilder.Entity<Lecture>().HasData(MeetupData.Lectures);
            modelBuilder.Entity<Location>().HasData(MeetupData.Locations);
        }

    }
}
