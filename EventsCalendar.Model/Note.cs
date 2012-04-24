namespace EventsCalendar.Model
{
    using System;

    public class Note
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }

        public User Author { get; set; }
        public Post Post { get; set; }
    }
}
