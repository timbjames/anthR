using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using anthR.Web.Models.Core;

namespace anthR.Web.Controllers
{
    public class StaffOnProjectsController : Controller
    {
        private anthRContext db = new anthRContext();

        // GET: StaffOnProjects
        public async Task<ActionResult> Index()
        {
            var staffOnProjects = db.StaffOnProjects.Include(s => s.Project).Include(s => s.Staff);
            return View(await staffOnProjects.ToListAsync());
        }

        // GET: StaffOnProjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffOnProjects staffOnProjects = await db.StaffOnProjects.FindAsync(id);
            if (staffOnProjects == null)
            {
                return HttpNotFound();
            }
            return View(staffOnProjects);
        }

        // GET: StaffOnProjects/Create
        public ActionResult Create(int? id)
        {
            ViewBag.Projects = new SelectList(db.Project, "Id", "Name", (id.HasValue ? id.Value : 0));
            ViewBag.StaffId = new SelectList(db.Staff, "Id", "Name");            
            return View();
        }

        // POST: StaffOnProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StaffId,ProjectId")] StaffOnProjects staffOnProjects)
        {
            if (ModelState.IsValid)
            {
                db.StaffOnProjects.Add(staffOnProjects);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", staffOnProjects.ProjectId);
            ViewBag.StaffId = new SelectList(db.Staff, "Id", "Name", staffOnProjects.StaffId);
            return View(staffOnProjects);
        }

        // GET: StaffOnProjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffOnProjects staffOnProjects = await db.StaffOnProjects.FindAsync(id);
            if (staffOnProjects == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", staffOnProjects.ProjectId);
            ViewBag.StaffId = new SelectList(db.Staff, "Id", "Name", staffOnProjects.StaffId);
            return View(staffOnProjects);
        }

        // POST: StaffOnProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StaffId,ProjectId")] StaffOnProjects staffOnProjects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffOnProjects).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", staffOnProjects.ProjectId);
            ViewBag.StaffId = new SelectList(db.Staff, "Id", "Name", staffOnProjects.StaffId);
            return View(staffOnProjects);
        }

        // GET: StaffOnProjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffOnProjects staffOnProjects = await db.StaffOnProjects.FindAsync(id);
            if (staffOnProjects == null)
            {
                return HttpNotFound();
            }
            return View(staffOnProjects);
        }

        // POST: StaffOnProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StaffOnProjects staffOnProjects = await db.StaffOnProjects.FindAsync(id);
            db.StaffOnProjects.Remove(staffOnProjects);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
