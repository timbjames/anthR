using System;
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}