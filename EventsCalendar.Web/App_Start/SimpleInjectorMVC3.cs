[assembly: WebActivator.PreApplicationStartMethod(typeof(EventsCalendar.Web.App_Start.SimpleInjectorMVC3), "Initialize")]

namespace EventsCalendar.Web.App_Start
{
    using System.Reflection;
    using EventsCalendar.Repository;
    using SimpleInjector;
    
    public class SimpleInjectorMVC3
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();

            ContainerRegistration.InitializeContainer(container);

            container.RegisterControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            container.RegisterAsMvcDependencyResolver();
            container.RegisterAttributeFilterProvider();
        }
     }
}