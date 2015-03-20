using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anthR.Web.Models.arTask;
using System.ComponentModel.DataAnnotations;

namespace anthR.Web.Models.Core
{
    public class Project
    {
       
        public int Id { get; set; }
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        public bool Completed { get; set; }
        public DateTime? Deadline { get; set; }
        [Display(Name = "Planned Start")]
        public DateTime PlannedStart { get; set; }
        [Display(Name = "Date Completed")]
        public DateTime? DateCompleted { get; set; }
        [Display(Name = "On Going Project?")]
        public bool OnGoing { get; set; }

        public virtual ICollection<StaffOnProjects> StaffOnProjects { get; set; }
        [Display(Name = "Master Site")]
        public int MasterSiteId { get; set; }
        public virtual MasterSite MasterSite { get; set; }
        public virtual ICollection<AnthRTask> Tasks { get; set; }

        public string Username { get; set; }

        public double Quoted { get; set; }
        public bool AlreadyBilled { get; set; }

    }
}
