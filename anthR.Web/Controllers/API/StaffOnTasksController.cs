using anthR.Web.Models.Core;
using anthR.Web.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace anthR.Web.Controllers.API
{
    public class StaffOnTasksController : ApiController
    {
                
        private anthRContext _db = new anthRContext();
                        
        public HttpResponseMessage Post(StaffToAdd staffToAdd)
        {

            Notification notificationService = null;
            // for each staff id in the staffid array, add to the db.
            foreach (var staffId in staffToAdd.StaffId)
            {
                // add staff
                _db.StaffOnTask.Add(new StaffOnTask() { StaffId = staffId, AnthRTaskId = staffToAdd.AnthRTaskId });

                // send off email to the staff member informing them of the new task
                notificationService = new Notification();
                notificationService.SendTaskEmail(staffToAdd.AnthRTaskId, staffId);

            }
            _db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);

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

}
