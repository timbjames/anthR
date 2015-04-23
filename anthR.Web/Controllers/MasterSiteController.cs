using anthR.Web.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

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
                HasVAT = model.HasVAT,
                LiveBidMasterSiteId = model.LiveBidMasterSiteId
            };

            _db.MasterSite.Add(masterSite);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Edit(int id)
        {
            return View(await _db.MasterSite.Where(m => m.MasterSiteId.Equals(id)).FirstOrDefaultAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "MasterSiteId, LiveBidMasterSiteId, Name, HasVAT")] MasterSite masterSite)
        {
            
            if (ModelState.IsValid)
            {
                _db.Entry(masterSite).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(masterSite);

        }

        public async Task<ActionResult> Details(int id)
        {
            return View(await _db.MasterSite.Where(m => m.MasterSiteId.Equals(id)).FirstOrDefaultAsync());
        }

    }
}