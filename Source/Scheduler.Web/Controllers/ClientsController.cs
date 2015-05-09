using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Scheduler.Common;
using Scheduler.Common.DataAccess;

namespace Scheduler.Web.Controllers
{
    public class ClientsController : Controller
    {
        public ClientsController()
        {
            // HACK in order to trigger migration of the database
            using (var context = new SchedulerContext())
            {
                var dummyAccess = context.Clients.ToList();
            }
        }

        // GET: Clients
        public ActionResult Index()
        {
            return View();
        }
    }
}