namespace EventsCalendar.Repository
{
    using System.Data.Entity;
    using EventsCalendar.Model;

    public class CalendarDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
