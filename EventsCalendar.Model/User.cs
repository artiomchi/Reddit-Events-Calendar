namespace EventsCalendar.Model
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            Created = DateTime.Now;
        }

        public int ID { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public Guid PasswordSalt { get; set; }
        public Guid PasswordHash { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual CalendarAuthorization CalendarAuthorization { get; set; }
    }
}
