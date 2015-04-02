using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anthR.Web.Controllers
{
    public class HomeController : Controller
    {

        private Models.Core.anthRContext _context = null;
        private List<Models.Todo.TodoItem> _todoItems = null;
        
        public HomeController()
        {
            _context = new Models.Core.anthRContext();
            _todoItems = _context.TodoItems.ToList();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {            
            return View();
        }
           
        public ActionResult EmailJson()
        {

            ArrayList al = new ArrayList();
            al.Add(new EmailObject()
            {
                avatar = "https://secure.gravatar.com/avatar/10247f791f5065a8e090804c1ecc1cea?s=30&amp;d=mm",
                content = "this is the content",
                desc = "this is the desc",
                name = "tim james",
                subject = "this is the subject",
                timestamp = "12345",
                unread = "false"
            });
            return Json(al, JsonRequestBehavior.AllowGet);

        }

    }

    public class EmailObject
    {
        public string subject { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string timestamp { get; set; }
        public string avatar { get; set; }
        public string unread { get; set; }
        public string desc { get; set; }
    }

}