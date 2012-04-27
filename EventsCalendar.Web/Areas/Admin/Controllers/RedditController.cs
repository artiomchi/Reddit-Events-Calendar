using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsCalendar.Repository;
using EventsCalendar.Web.Areas.Admin.Models.Reddit;

namespace EventsCalendar.Web.Areas.Admin.Controllers
{
    public partial class RedditController : Controller
    {
        private RedditRepository _redditRepository = new RedditRepository();

        //
        // GET: /Admin/Reddit/
        public virtual ActionResult Index()
        {
            var model = new IndexModel
            {
                BotLogin = SettingsRepository.Reddit.BotLogin,
                BotPassword = SettingsRepository.Reddit.BotPassword != null ? "[secured]" : null,
            };
            return View(model);
        }

        //
        // JSON POST: /Admin/Reddit/TestCredentials
        [HttpPost]
        public virtual ActionResult TestCredentials()
        {
            if (SettingsRepository.Reddit.BotLogin == null)
                return Json(new { Success = false, Message = "No Login!" });
            if (SettingsRepository.Reddit.BotPassword == null)
                return Json(new { Success = false, Message = "No Password" });

            var result = _redditRepository.TestCredentials(SettingsRepository.Reddit.BotLogin, SettingsRepository.Reddit.BotPassword);
            return Json(new { Success = result.Status, Message = result.Message });
        }
    }
}
