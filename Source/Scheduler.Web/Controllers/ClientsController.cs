using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Scheduler.Common.DataAccess;

namespace Scheduler.Web.Controllers
{
    public class ClientsController : Controller
    {
        // GET: Clients
        public ActionResult Index()
        {
            using (var dbContext = new SchedulerContext())
            {
                var test = dbContext.ConnectedClients.ToList();
                //var connectedClients = await dbContext.ConnectedClients.ToListAsync();
                return View();
            }
        }
    }
}