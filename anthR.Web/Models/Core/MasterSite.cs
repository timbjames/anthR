using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Core
{
    public class MasterSite
    {

        public int MasterSiteId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Todo.TodoItem> TodoItems { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}
