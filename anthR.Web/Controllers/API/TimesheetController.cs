using anthR.Web.Models.arTask;
using anthR.Web.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace anthR.Web.Controllers.API
{
    public class TimesheetController : ApiController
    {

        private anthRContext _db;

        public TimesheetController()
        {
            _db = new anthRContext();
        }

        // GET: api/Timesheet
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Timesheet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Timesheet
        public HttpResponseMessage Post(HideFromTimesheet hideFromTimesheet)
        {

            switch (hideFromTimesheet.Type)
            {
                case 1:
                    // project
                    Project project = null;
                    project = _db.Project.Where(p => p.Id.Equals(hideFromTimesheet.Id)).FirstOrDefault();
                    project.HideFromTimesheet = !hideFromTimesheet.Hide;
                    break;
                case 2:
                    // task
                    AnthRTask task = null;
                    task = _db.AnthRTask.Where(at => at.Id.Equals(hideFromTimesheet.Id)).FirstOrDefault();
                    task.HideFromTimesheet = !hideFromTimesheet.Hide;
                    break;
                case 3:
                    // timesheet
                    Timesheet timesheet = null;
                    timesheet = _db.Timesheets.Where(t => t.Id.Equals(hideFromTimesheet.Id)).FirstOrDefault();
                    timesheet.HideFromTimesheet = !hideFromTimesheet.Hide;
                    break;
                        
            }

            _db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        // PUT: api/Timesheet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Timesheet/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
            base.Dispose(disposing);
        }

    }

    public class HideFromTimesheet
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public bool Hide { get; set; }
    }

}
