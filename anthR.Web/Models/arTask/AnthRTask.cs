using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anthR.Web.Models.Core;
using System.ComponentModel.DataAnnotations;

namespace anthR.Web.Models.arTask
{
    public class AnthRTask
    {
        
        public int Id { get; set; }

        public string Username { get; set; }

        [Display(Name = "Task")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Deadline { get; set; }

        [Display(Name = "Planned Start")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime PlannedStart { get; set; }

        [Display(Name = "Date Completed")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? DateCompleted { get; set; }

        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; }
        [Display(Name = "Agreed With")]
        public string AgreedWith { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<StaffOnTask> StaffOnTasks { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public int Priority { get; set; }

        public virtual ICollection<Timesheet> Timesheet { get; set; }
        public bool? HideFromTimesheet { get; set; }

    }
}
