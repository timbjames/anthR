using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.arTask
{
    
    /// <summary>
    /// Used to populate the Bootstrap Calendar
    /// </summary>
    public class ScheduleResult
    {        
        public int success { get; set; }
        public List<Events> result { get; set; }
    }

    public class Events
    {
        public int id { get; set; }
        public long start { get; set; }
        public long end { get; set; }
        public string theClass { get; set; }
        public string title { get; set; }
        public string url { get; set; }
    }

}
