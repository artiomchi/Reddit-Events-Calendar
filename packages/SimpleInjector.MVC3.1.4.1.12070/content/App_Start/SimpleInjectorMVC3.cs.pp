[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.SimpleInjectorMVC3), "Initialize")]

namespace $rootnamespace$.App_Start
{
    using System.Reflection;
    using SimpleInjector;
    
    public class SimpleInjectorMVC3
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            
            InitializeContainer(container);

            container.RegisterControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            container.RegisterAsMvcDependencyResolver();
            container.RegisterAttributeFilterProvider();        
        }
     
        private static void InitializeContainer(Container container)
        {
#error Register your services here (remove this line).

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>();
        }
    }
}