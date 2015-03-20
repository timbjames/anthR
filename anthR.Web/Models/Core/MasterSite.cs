using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Core
{
    public class MasterSite
    {
                
        [Display(Name = "Master Site")]
        public int MasterSiteId { get; set; }
        public string Name { get; set; }
        public int LiveBidMasterSiteId { get; set; }

        public bool HasVAT { get; set; }

        public virtual ICollection<Todo.TodoItem> TodoItems { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}
