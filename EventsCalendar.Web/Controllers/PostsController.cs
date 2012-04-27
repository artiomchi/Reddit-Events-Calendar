using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsCalendar.Repository;

namespace EventsCalendar.Web.Controllers
{
    public partial class PostsController : Controller
    {
        IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        //
        // GET: /
        public virtual ActionResult Index()
        {
            var posts = _postRepository.GetTopPosts();
            return View(posts);
        }

        //
        // GET: /details/sfp48/{title}
        public virtual ActionResult Details(string rid)
        {
            var post = _postRepository.GetPost(rid);
            return View(post);
        }
    }
}
