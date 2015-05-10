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
        }

        // GET: Clients
        public ActionResult Index()
        {
            return View(_schedulerContext.Clients);
        }

        public ActionResult Details(string id)
        {
            ViewBag.AvailableCommands = _schedulerContext.Commands;
            return View(_schedulerContext.Clients.FirstOrDefault(c => c.Name == id));
        }

    }
}