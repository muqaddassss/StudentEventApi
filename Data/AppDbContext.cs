using Microsoft.EntityFrameworkCore;
using StudentEventAPI.Models;
using Microsoft.EntityFrameworkCore;
using StudentEventAPI.Models;

namespace StudentEventAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; } // ✅ Added

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Define composite key and relationships for EventParticipant
            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.EventId, ep.ParticipantId });

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.EventParticipants)
                .HasForeignKey(ep => ep.EventId);

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Participant)
                .WithMany(p => p.EventParticipants)
                .HasForeignKey(ep => ep.ParticipantId);
        }
    }
}

