﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anthR.Web.Models.Core;
using System.ComponentModel.DataAnnotations;

namespace anthR.Web.Models.Task
{
    public class AnthRTask
    {
        
        public int Id { get; set; }       
        [Display(Name = "Task")]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime Deadline { get; set; }
        [Display(Name = "Planned Start")]
        public DateTime PlannedStart { get; set; }
        [Display(Name = "Date Completed")]
        public DateTime DateCompleted { get; set; }

        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; }
        [Display(Name = "Agreed With")]
        public string AgreedWith { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<StaffOnTask> StaffOnTasks { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

    }
}
