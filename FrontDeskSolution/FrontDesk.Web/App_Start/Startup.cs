using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using StructureMap;
using System;
using System.Linq;

[assembly: OwinStartup(typeof(FrontDesk.Web.AppStart.Startup))]
namespace FrontDesk.Web.AppStart
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var resolver = ObjectFactory.GetInstance<IDependencyResolver>();
            //var config = new HubConfiguration { Resolver = resolver };
            app.MapSignalR();
        }
    }
}