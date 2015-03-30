using anthR.Web.Models.arTask;
using anthR.Web.Models.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace anthR.Web.Controllers.API
{
    public class ScheduleController : ApiController
    {

        private anthRContext _db = new anthRContext();

        public ScheduleResult Get()
        {

            int? id = null;

            List<AnthRTask> anthRTask = null;

            anthRTask = _db.AnthRTask.Where(
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
                .Include(a => a.Project).ToList();

            ScheduleResult result = new ScheduleResult();
            result.success = 1;
            result.result = anthRTask.Select(s => new Events()
            {
                end = s.Deadline.ToJavaScriptMilliseconds(),                
                start = s.PlannedStart.ToJavaScriptMilliseconds(),                
                id = s.Id,
                theClass = string.Format("event-priority-{0}", s.Priority),
                title = string.Format("{0} - {1} - {2}", s.Project.MasterSite.Name, s.Project.Name, s.Name),
                url = string.Format("/tasks/details/{0}", s.Id)
            }).ToList();

            return result;

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
    
}
