using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Core
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HexColor { get; set; }
        public string Glyphicon { get; set; }
        public bool ShowIcon { get; set; }
    }
}
