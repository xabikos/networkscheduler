using System;
using System.Linq;
using System.Web.Mvc;
using Scheduler.Common;
using Scheduler.Common.DataAccess;

namespace Scheduler.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IConnectedClientsRegistry _clientsRegistry;

        public ClientsController()
        {
            _clientsRegistry = new ConnectedClientsRegistry();
            // HACK in order to trigger migration of the database
            using (var context = new SchedulerContext())
            {
                var dummyAccess = context.Clients.ToList();
            }
        }

        // GET: Clients
        public ActionResult Index()
        {
            return View(_clientsRegistry.GetConnectedClients().Select(cc => cc.Client));
        }
    }
}