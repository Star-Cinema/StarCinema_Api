using StarCinema_Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace StarCinema_Api.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        #region DB set
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Films> Films { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Schedules> Schedules { get; set; }
        public DbSet<Seats> Seats { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        #endregion 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
