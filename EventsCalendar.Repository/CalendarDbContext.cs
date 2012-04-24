namespace EventsCalendar.Repository
{
    using System.Data.Entity;
    using EventsCalendar.Model;

    public class CalendarDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Note> Notes { get; set; }

        public class Initializer : DropCreateDatabaseIfModelChanges<CalendarDbContext>
        {
            protected override void Seed(CalendarDbContext context)
            {
                var posts = new RedditRepository().GetTopPosts();

                foreach (var p in posts)
                {
                    context.Posts.Add(p);
                }
                context.SaveChanges();
            }
        }
    }
}
