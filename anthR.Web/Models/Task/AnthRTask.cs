using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anthR.Web.Models.Core;

namespace anthR.Web.Models.Task
{
    public class AnthRTask
    {
        
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<StaffOnTask> StaffOnTasks { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

    }
}
