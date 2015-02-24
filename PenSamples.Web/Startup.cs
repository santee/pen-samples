using Microsoft.Owin;

using PenSamples.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace PenSamples.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}