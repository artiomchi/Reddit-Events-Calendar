using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsCalendar.Model;

namespace EventsCalendar.Repository
{
    public class PostRepository : RepositoryBase<CalendarDbContext>, EventsCalendar.Repository.IPostRepository
    {
        public IQueryable<Post> GetTopPosts()
        {
            //using (DataContext)
            {
                return DataContext.Posts
                    .Include(p => p.Author)
                    .OrderBy(p => p.Meeting.HasValue)
                    .ThenByDescending(p => p.Meeting);
            }
        }

        public Post GetPost(string rid)
        {
            //using (DataContext)
            {
                return DataContext.Posts
                    .Include(p => p.Author)
                    .Include(p => p.Notes)
                    .Include("Notes.Author")
                    .SingleOrDefault(p => p.RID == rid);
            }
        }
    }
}
