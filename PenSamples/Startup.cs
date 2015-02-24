using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PenSamples.Startup))]
namespace PenSamples
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
