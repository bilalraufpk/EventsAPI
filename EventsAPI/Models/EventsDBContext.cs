using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EventsAPI.Models
{
    public partial class EventsDBContext : DbContext
    {
        public EventsDBContext()
        {
        }

        public EventsDBContext(DbContextOptions<EventsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Startup.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => new { e.Title, e.UserId, e.Date })
                    .HasName("UC_User_Event_Date")
                    .IsUnique();

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Users");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => new { e.EventId, e.UserId })
                    .HasName("UC_Order_Event_User")
                    .IsUnique();

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Events");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UC_User_Email")
                    .IsUnique();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Role).IsUnicode(false);
            });
        }
    }
}
