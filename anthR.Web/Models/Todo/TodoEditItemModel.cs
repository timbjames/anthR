using anthR.Web.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Todo
{
    public class TodoItemEditModel
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsDone { get; set; }
        public int Priority { get; set; }
        public int MastersiteId { get; set; }
        public List<MasterSite> MasterSites { get; set; }

    }
}
