namespace EventsCalendar.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public int ID { get; set; }
        [Required]
        public string RID { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.Html)]
        public string SelfTextHtml { get; set; }
        public string SelfText { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Meeting { get; set; }

        public string Event { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public int? AuthorID { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
