using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anthR.Web.Models.Task;
using System.ComponentModel.DataAnnotations;

namespace anthR.Web.Models.Core
{
    public class Project
    {
       
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Completed { get; set; }
        public DateTime? Deadline { get; set; }
        [Display(Name = "Planned Start")]
        public DateTime PlannedStart { get; set; }
        [Display(Name = "Date Completed")]
        public DateTime? DateCompleted { get; set; }
        public bool OnGoing { get; set; }

        public virtual ICollection<StaffOnProjects> StaffOnProjects { get; set; }
        public int MasterSiteId { get; set; }
        public virtual MasterSite MasterSite { get; set; }
        public virtual ICollection<AnthRTask> Tasks { get; set; }

        public string Username { get; set; }

    }
}
