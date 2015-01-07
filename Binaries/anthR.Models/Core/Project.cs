using anthR.Models.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Models.Core
{
    public class Project
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
        /// <summary>
        /// Foreign Key
        /// </summary>
        [ForeignKey("MastersiteId")]
        public int MastersiteId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual MasterSite MasterSite { get; set; }

        public virtual ICollection<AnthRTask> Tasks { get; set; }

    }
}
