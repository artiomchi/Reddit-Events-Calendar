namespace EventsCalendar.Model
{
    using System;

    public class Note
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }

        public virtual User Author { get; set; }
        public virtual Post Post { get; set; }
    }
}
