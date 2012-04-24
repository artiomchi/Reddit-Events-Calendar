namespace EventsCalendar.Repository
{
    using System;

    public interface IPostRepository
    {
        EventsCalendar.Model.Post GetPost(string rid);
        System.Linq.IQueryable<EventsCalendar.Model.Post> GetTopPosts();
    }
}
