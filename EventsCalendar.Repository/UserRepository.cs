using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.OAuth2;
using EventsCalendar.Model;

namespace EventsCalendar.Repository
{
    public class UserRepository : RepositoryBase<CalendarDbContext>
    {
        public CalendarAuthorization GetCalendarAuthorization(string login)
        {
            var result = DataContext.CalendarAuthorizations
                .Where(c => c.User.Login == login)
                .SingleOrDefault();

            return result;
        }

        public OperationStatus UpdateCalendarAuthorization(String login, IAuthorizationState authorization)
        {
            if (authorization == null)
                return OperationStatus.CreateFromException("No authorization passed", new ArgumentNullException("authorization"));

            try
            {
                var dbAuth = authorization as CalendarAuthorization;
                if (dbAuth == null)
                {
                    dbAuth = new CalendarAuthorization
                    {
                        AccessToken = authorization.AccessToken,
                        AccessTokenExpirationUtc = authorization.AccessTokenExpirationUtc,
                        AccessTokenIssueDateUtc = authorization.AccessTokenIssueDateUtc,
                        Callback = authorization.Callback,
                        RefreshToken = authorization.RefreshToken,
                    };
                }

                var user = DataContext.Users.Single(u => u.Login == login);
                user.CalendarAuthorization = dbAuth;
                DataContext.SaveChanges();

                return new OperationStatus { Status = true };
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromException("Could not save authorization", ex);
            }
        }

        public OperationStatus ResetCalendarAuthorization(String login)
        {
            var auth = DataContext.CalendarAuthorizations.SingleOrDefault(a => a.User.Login == login);
            if (auth != null)
            {
                DataContext.CalendarAuthorizations.Remove(auth);
                DataContext.SaveChanges();
            }
            return new OperationStatus { Status = true };
        }
    }
}
