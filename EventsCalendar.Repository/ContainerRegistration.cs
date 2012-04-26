using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace EventsCalendar.Repository
{
    /// <summary>
    /// Class that ties the repository interfaces to actual live repositories
    /// http://simpleinjector.codeplex.com/wikipage?title=MVC%20Integration&referringTitle=Integration%20Guide
    /// http://simpleinjector.codeplex.com/wikipage?title=Using%20the%20Simple%20Injector&referringTitle=Home
    /// </summary>
    /// <param name="container">The SimpleInjector container registered in the project</param>
    public static class ContainerRegistration
    {
        public static void InitializeContainer(Container container)
        {
            container.Register<IPostRepository, PostRepository>();
        }
    }
}
