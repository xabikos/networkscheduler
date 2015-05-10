using System;
using System.Linq;
using System.Web.Mvc;
using Scheduler.Common.DataAccess;
using System.Data.Entity;
using Scheduler.Common.Logging;

namespace Scheduler.Web.Controllers
{
    public class LogsController : Controller
    {
        private readonly SchedulerContext _schedulerContext;

        public LogsController(SchedulerContext schedulerContext)
        {
            _schedulerContext = schedulerContext;
        }

        // GET: Logs
        public ActionResult Index()
        {
            // Eager load the command and client detail as we know we need the info and prevent additional trips to the database
            return View(_schedulerContext.CommandsExecutuions.Include(ce => ce.Command).Include(ce => ce.Client));
        }

        public ActionResult Details(int id)
        {
            ViewBag.Execution = _schedulerContext.CommandsExecutuions.First(ce => ce.Id == id);
            return View(_schedulerContext.LogEntries.OfType<ExecutionLogEntry>().Where(le => le.ExecutionId == id));
        }

    }
}