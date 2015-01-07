using anthR.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Models.Todo
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// Foreign Key
        /// </summary>       
        //[ForeignKey("MastersiteId")]
        //public int MastersiteId { get; set; }

        /// <summary>
        /// Navigation Properties
        /// </summary>
        //[ForeignKey("MastersiteId")]
        //public virtual MasterSite MasterSite { get; set; }
    }
}
