using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VetClinicPublic.Web.Startup))]
namespace VetClinicPublic.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
