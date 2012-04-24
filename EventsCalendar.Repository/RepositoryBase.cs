using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCalendar.Repository
{
    public class RepositoryBase<C> : IDisposable
        where C : DbContext, new()
    {
        private C _dataContext;

        public virtual C DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new C();
                    this.AllowSerialization = true;
                }
                return _dataContext;
            }
        }

        public bool AllowSerialization
        {
            get
            {
                return _dataContext.Configuration.ProxyCreationEnabled;
            }
            set
            {
                _dataContext.Configuration.ProxyCreationEnabled = !value;
            }
        }

        public void Dispose()
        {
            if (_dataContext != null)
            {
                _dataContext.Dispose();
                _dataContext = null;
            }
        }
    }
}
