using anthR.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Models.Tasks
{
    public class AnthRTask
    {

        public int Id { get; set; }

        /// <summary>
        /// Foreign key
        /// </summary>
        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }    
            
        /// <summary>
        /// navigation property
        /// </summary>
        public virtual Project Project { get; set; }
                
        /// <summary>
        /// navigation property
        /// </summary>
        public virtual StaffOnTasks StaffOnTasks { get; set; }    

    }
}
