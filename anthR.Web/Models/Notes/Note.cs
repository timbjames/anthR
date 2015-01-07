using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Notes
{
    public class Note
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int StaffId { get; set; }
        public virtual Core.Staff Staff { get; set; }

    }
}
