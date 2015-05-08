using System;
using Microsoft.Owin.Cors;
using Owin;

namespace Scheduler.Server.SignalR
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
