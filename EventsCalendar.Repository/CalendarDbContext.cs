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
        public DbSet<CalendarAuthorization> CalendarAuthorizations { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public class Initializer : DropCreateDatabaseIfModelChanges<CalendarDbContext>
        {
            protected override void Seed(CalendarDbContext context)
            {
                var posts = new RedditRepository().GetTopPosts();

                foreach (var p in posts)
                {
                    context.Posts.Add(p);
                }

                context.Users.Add(new User
                {
                    Login = "LscCalendarBot",
                    Name = "LscCalendarBot",
                });
                context.Settings.Add(new Setting { Name = "Reddit.BotLogin", Value = "LscCalendarBot" });
                context.Settings.Add(new Setting { Name = "GoogleAPI.ClientID", Value = "679429737211-of2ab9qhcdij0aukgb3hqk3rh0bu76bk.apps.googleusercontent.com" });
                context.Settings.Add(new Setting { Name = "GoogleAPI.ClientSecret", Value = "4owNvvuS6NtVI-9yOm9Ldsmu" });
                context.Settings.Add(new Setting { Name = "GoogleAPI.ApiKey", Value = "AIzaSyDpDdn_U8_wznqcTDs3Q0hf7vCAnRmD04w" });
                context.SaveChanges();
            }
        }
    }
}
