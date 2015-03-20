using anthR.Web.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace anthR.Web.Controllers.API
{
 
    [EnableCors(origins: "http://localhost:35438", headers: "*", methods: "*")]
    public class StaffOnProjectsController : ApiController
    {

        private anthRContext _db = new anthRContext();

        public string Get()
        {
            return "Hello World";
        }
                
        public HttpResponseMessage Post(StaffToAdd staffToAdd)
        {     
       
            // for each staff id in the staffid array, add to the db.
            foreach (var staffId in staffToAdd.StaffId)
            {
                // add staff
                _db.StaffOnProjects.Add(new StaffOnProjects() { StaffId = staffId, ProjectId = staffToAdd.ProjectId });
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

    public class StaffToAdd
    {
        public int[] StaffId { get; set; }
        public int ProjectId { get; set; }
        public int AnthRTaskId { get; set; }
    }

}
