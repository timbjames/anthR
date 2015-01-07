using anthR.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Models.Core
{
    public class MasterSite
    {

        public int MastersiteId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Nagivation property
        /// </summary>
        public virtual ICollection<TodoItem> TodoItems { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        //public virtual ICollection<Project> Projects { get; set; }

        public MasterSite()
        {
            TodoItems = new List<TodoItem>();
            //Projects = new List<Project>();
        }

    }
}
