using System;
using System.Linq;
using System.Web.Mvc;
using Scheduler.Common.DataAccess;

namespace Scheduler.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly SchedulerContext _schedulerContext;

        public ClientsController(SchedulerContext schedulerContext)
        {
            _schedulerContext = schedulerContext;
            // HACK in order to trigger migration of the database
            var dummyAccess = _schedulerContext.Clients.ToList();
            
        }

        // GET: Clients
        public ActionResult Index()
        {
            return View(_schedulerContext.Clients);
        }

    }
}