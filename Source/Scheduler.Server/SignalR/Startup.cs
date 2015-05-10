using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Cors;
using Owin;
using Scheduler.Server.Services;


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