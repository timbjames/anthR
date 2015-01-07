using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Core
{
    
    public class StaffOnTask
    {

        [Key]
        [Column(Order = 0)]
        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }

        [Key]
        [Column(Order = 1)]
        public int AnthRTaskId { get; set; }
        public virtual Task.AnthRTask AnthRTask { get; set; }

    }

}
