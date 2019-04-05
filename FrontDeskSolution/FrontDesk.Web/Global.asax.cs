using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AppointmentScheduling.Data;
using ClientPatientManagement.Data;
using FrontDesk.Web.AppStart;
using StructureMap;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using FrontDesk.Web.Hubs;
using FrontDesk.Web.App_Start;

namespace FrontDesk.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<CrudContext>(null);
            Database.SetInitializer<SchedulingContext>(null);

            IContainer container = StructureMap.ObjectFactory.Container;
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(container));

            MessagingConfig.StartCheckingMessages();

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            StructureMap.ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }
}