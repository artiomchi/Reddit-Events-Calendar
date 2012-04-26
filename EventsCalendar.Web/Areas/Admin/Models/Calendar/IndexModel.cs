using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsCalendar.Web.Areas.Admin.Models.Calendar
{
    public class IndexModel
    {
        public bool TokenPresent;
        public bool TokenInvalid;
        public Google.Apis.Calendar.v3.Data.CalendarList Calendars;
    }
}