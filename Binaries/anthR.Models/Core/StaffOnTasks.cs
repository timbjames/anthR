using anthR.Models.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Models.Core
{
    public class StaffOnTasks
    {

         /// <summary>
        /// Foreign key
        /// </summary>
        [ForeignKey("StaffId")]
        public int StaffId { get; set; }

        /// <summary>
        /// Foreign Key
        /// </summary>
        [ForeignKey("TaskId")]
        public int TaskId { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual AnthRTask Task { get; set; }

    }
}
