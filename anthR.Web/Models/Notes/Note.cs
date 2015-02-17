using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Notes
{
    public class Note
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public int StaffId { get; set; }
        public virtual Core.Staff Staff { get; set; }

        public string Username { get; set; }

    }
}
