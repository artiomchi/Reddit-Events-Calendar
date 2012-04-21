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
        public DateTime Created { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
