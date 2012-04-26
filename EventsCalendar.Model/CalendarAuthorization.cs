using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util;

namespace EventsCalendar.Model
{
    public class CalendarAuthorization : IAuthorizationState
    {
        public CalendarAuthorization()
        {
            Scope = new HashSet<string>(OAuthUtilities.ScopeStringComparer) { CalendarService.Scopes.Calendar.GetStringValue() };
        }

        public string AccessToken { get; set; }
        public DateTime? AccessTokenExpirationUtc { get; set; }
        public DateTime? AccessTokenIssueDateUtc { get; set; }
        public string RefreshToken { get; set; }
        public string CallbackURI { get; set; }

        [NotMapped]
        public Uri Callback { get { return new Uri(CallbackURI); } set { CallbackURI = value.ToString(); } }
        [NotMapped]
        public HashSet<string> Scope { get; private set; }

        [Key, ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
