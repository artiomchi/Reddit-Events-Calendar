namespace EventsCalendar.Model
{
    using System.Collections.Generic;

    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
