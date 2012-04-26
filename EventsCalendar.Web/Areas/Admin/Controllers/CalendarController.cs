using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;
using EventsCalendar.Repository;
using EventsCalendar.Web.Areas.Admin.Models.Calendar;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util;

namespace EventsCalendar.Web.Areas.Admin.Controllers
{
    public class CalendarController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (_client == null)
                _client = new WebServerClient(GoogleAuthenticationServer.Description)
                {
                    ClientIdentifier = SettingsRepository.GoogleAPI.ClientID,
                    ClientSecret = SettingsRepository.GoogleAPI.ClientSecret,
                };
            if (_authenticator == null)
                _authenticator = new OAuth2Authenticator<WebServerClient>(_client, GetAuthorization);
            if (_service == null)
                _service = new CalendarService(_authenticator);
        }

        private static CalendarService _service;
        private static WebServerClient _client;
        private static OAuth2Authenticator<WebServerClient> _authenticator;
        private IAuthorizationState _state;
        private UserRepository _userRepository = new UserRepository();

        private IAuthorizationState GetAuthorization(WebServerClient client)
        {
            // If this user is already authenticated, then just return the auth state.
            IAuthorizationState state = _state ?? _userRepository.GetCalendarAuthorization(SettingsRepository.Reddit.BotLogin);
            if (state != null)
                return state;

            // Check if an authorization request already is in progress.
            System.Threading.Thread.Sleep(2000);
            state = client.ProcessUserAuthorization();
            if (state != null && (!string.IsNullOrEmpty(state.AccessToken) || !string.IsNullOrEmpty(state.RefreshToken)))
            {
                // Store and return the credentials.
                _userRepository.UpdateCalendarAuthorization(SettingsRepository.Reddit.BotLogin, _state = state);
                return state;
            }

            // Otherwise do a new authorization request.
            return null;
        }

        //
        // GET: /Admin/Calendar
        public ActionResult Index()
        {
            var model = new IndexModel
            {
                TokenPresent = _userRepository.GetCalendarAuthorization(SettingsRepository.Reddit.BotLogin) != null,
            };

            if (model.TokenPresent)
            {
                try
                {
                    model.Calendars = _service.CalendarList.List().Fetch();
                }
                catch (Google.GoogleApiRequestException)
                {
                    model.TokenInvalid = true;
                }
            }

            return View(model);
        }

        //
        // GET: /Admin/Calendar/Authorize
        public ActionResult Authorize(String code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                var oldState = _authenticator.State;
                _authenticator.LoadAccessToken();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Calendar/Authorize
        [HttpPost]
        public ActionResult Authorize()
        {
            _userRepository.ResetCalendarAuthorization(SettingsRepository.Reddit.BotLogin);
            string scope = CalendarService.Scopes.Calendar.GetStringValue();
            OutgoingWebResponse response = _client.PrepareRequestUserAuthorization(new[] { scope });
            response.Send(); // Will throw a ThreadAbortException to prevent sending another response.
            return null;
        }
    }
}
