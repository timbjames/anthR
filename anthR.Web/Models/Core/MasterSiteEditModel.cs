using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Core
{
    public class MasterSiteEditModel
    {
        public int MasterSiteId { get; set; }
        public string Name { get; set; }
        public int LiveBidMasterSiteId { get; set; }
    }
}
