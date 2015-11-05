using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Bug
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public int IssueId { get; set; }
        public bool RaisedTask { get; set; }        
    }
}
