using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Scheduler.Web.Startup))]
namespace Scheduler.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
