using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anthR.Web.Models.Task;

namespace anthR.Web.Models.Core
{
    public class Project
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<StaffOnProjects> StaffOnProjects { get; set; }
        public int MasterSiteId { get; set; }
        public virtual MasterSite MasterSite { get; set; }
        public virtual ICollection<AnthRTask> Tasks { get; set; }

    }
}
