using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using anthR.Web.Models.Core;
using anthR.Web.Models.arTask;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization.Json;

namespace anthR.Web.Controllers
{
    public class ScheduleController : Controller
    {
                
        private anthRContext _db = new anthRContext();
        
        // GET: Scedule
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetSchedule(int? id)
        {

            List<AnthRTask> anthRTask = null;

            anthRTask = await _db.AnthRTask.Where(
                    t => !id.HasValue 
                    && !t.DateCompleted.HasValue 
                    && (
                        t.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) 
                        || t.StaffOnTasks.Where(st => st.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Any()
                        )
                    || t.ProjectId.Equals(id.Value) 
                    && !t.DateCompleted.HasValue
                    && (
                        t.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) 
                        || t.StaffOnTasks.Where(st => st.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Any()
                        )
                )                
                .OrderBy(p => p.Priority)
                .ThenBy(p => p.Deadline)                
                .ThenBy(p => p.PlannedStart)                
                .Include(a => a.Project).ToListAsync();

            ScheduleResult result = new ScheduleResult();
            result.success = 1;
            result.result = anthRTask.Select(s => new Events()
            {
                end = s.Deadline.ToJavaScriptMilliseconds(),                
                start = s.PlannedStart.ToJavaScriptMilliseconds(),                
                id = s.Id,
                theClass = string.Format("event-priority-{0}", s.Priority),
                title = string.Format("{0} - {1}", s.Project.MasterSite.Name, s.Project.Name),
                url = string.Format("/tasks/details/{0}", s.Id)
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }

    public class ScheduleResult
    {
        public int success { get; set; }
        public List<Events> result { get; set; }
    }

    public class Events
    {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }        
        public string theClass { get; set; }      
        public long start { get; set; }
        public long end { get; set; }
    }

}