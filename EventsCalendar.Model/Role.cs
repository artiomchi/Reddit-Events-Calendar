namespace EventsCalendar.Model
{
    using System.Collections.Generic;

    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
