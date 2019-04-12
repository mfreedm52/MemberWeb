using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Base2.Infrastructure;
using Base2.Infrastructure.Tasks;
using StructureMap;

namespace Base2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public IContainer Container
        {
            get
            {
                return (IContainer)HttpContext.Current.Items["_Container"];
            }
            set
            {
                HttpContext.Current.Items["_Container"] = value;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //      Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());



            DependencyResolver.SetResolver(
                new StructureMapDependencyResolver(() => Container ?? ObjectFactory.Container));


            //This is not according to the pluralsight video. Workaround to create our own objectfactory
            //http://stackoverflow.com/questions/25550914/how-to-use-container-instead-of-objectfactory-in-structuremap-serviceactivator/25551005#25551005
            ObjectFactory.Container.Configure(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new ControllerRegistry());
                cfg.AddRegistry(new ActionFilterRegistry(
                    () => Container ?? ObjectFactory.Container));
                cfg.AddRegistry(new MvcRegistry());
                cfg.AddRegistry(new TaskRegistry());
            });

            //This is now obsolete, need to learn new way to create container
            using (var container = ObjectFactory.Container.GetNestedContainer())
            {
                foreach (var task in container.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                }

                foreach (var task in container.GetAllInstances<IRunAtStartup>())
                {
                    task.Execute();
                }
            }
        }

        public void Application_BeginRequest()
        {

            //This is now obsolete, need to learn new way to create container
            Container = ObjectFactory.Container.GetNestedContainer();

            foreach (var task in Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            foreach (var task in Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }

        public void Application_EndRequest()
        {
            try
            {
                foreach (var task in
                    Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            finally
            {
                Container.Dispose();
                Container = null;
            }
        }
    }
}
