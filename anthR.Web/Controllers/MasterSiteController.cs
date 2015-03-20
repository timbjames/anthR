using anthR.Web.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anthR.Web.Controllers
{
    public class MasterSiteController : Controller
    {
        private readonly anthRContext _db;

        public MasterSiteController()
        {
            _db = new anthRContext();
        }

        // GET: /<controller>/
        public ActionResult Index()
        {
            return View(_db.MasterSite.ToList());
        }

        public ActionResult Create()
        {
            return View(new MasterSiteEditModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterSiteEditModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var masterSite = new MasterSite
            {
                Name = model.Name,                
                LiveBidMasterSiteId = model.LiveBidMasterSiteId
            };

            _db.MasterSite.Add(masterSite);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}