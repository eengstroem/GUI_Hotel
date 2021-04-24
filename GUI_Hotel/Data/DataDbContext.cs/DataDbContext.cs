using GUI_Hotel.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUI_Hotel.Data.DataDbContext
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; } 

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasMany(c => c.BookedRooms)
                .WithOne(f => f.Booking)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
