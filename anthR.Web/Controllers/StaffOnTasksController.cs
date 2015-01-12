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
    public class StaffOnTasksController : Controller
    {
        private anthRContext db = new anthRContext();

        // GET: StaffOnTasks
        public async Task<ActionResult> Index()
        {
            var staffOnTask = db.StaffOnTask.Include(s => s.AnthRTask).Include(s => s.Staff);
            return View(await staffOnTask.ToListAsync());
        }

        // GET: StaffOnTasks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffOnTask staffOnTask = await db.StaffOnTask.FindAsync(id);
            if (staffOnTask == null)
            {
                return HttpNotFound();
            }
            return View(staffOnTask);
        }

        // GET: StaffOnTasks/Create
        public ActionResult Create(int id, int? taskId)
        {

            // load staff only on the projectid passed in
            var staff = db.Staff.Where(s => s.StaffOnProjects.Where(p => p.ProjectId.Equals(id)).Any());

            ViewBag.AnthRTask = new SelectList(db.AnthRTask, "Id", "Name", (taskId.HasValue ? taskId.Value : 0));
            ViewBag.StaffId = new SelectList(staff.ToList(), "Id", "Name");

            return View();

        }

        // POST: StaffOnTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int id, [Bind(Include = "StaffId,AnthRTaskId")] StaffOnTask staffOnTask)
        {
            if (ModelState.IsValid)
            {
                db.StaffOnTask.Add(staffOnTask);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                //redirect to task index
                return RedirectToAction("Index", "Tasks", new { id = id });
            }

            var staff = db.Staff.Where(s => s.StaffOnProjects.Where(p => p.ProjectId.Equals(id)).Any());

            ViewBag.AnthRTaskId = new SelectList(db.AnthRTask, "Id", "Name", staffOnTask.AnthRTaskId);
            ViewBag.StaffId = new SelectList(staff.ToList(), "Id", "Name", staffOnTask.StaffId);
            return View(staffOnTask);
        }

        // GET: StaffOnTasks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffOnTask staffOnTask = await db.StaffOnTask.FindAsync(id);
            if (staffOnTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnthRTaskId = new SelectList(db.AnthRTask, "Id", "Name", staffOnTask.AnthRTaskId);
            ViewBag.StaffId = new SelectList(db.Staff, "Id", "Name", staffOnTask.StaffId);
            return View(staffOnTask);
        }

        // POST: StaffOnTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StaffId,AnthRTaskId")] StaffOnTask staffOnTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffOnTask).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AnthRTaskId = new SelectList(db.AnthRTask, "Id", "Name", staffOnTask.AnthRTaskId);
            ViewBag.StaffId = new SelectList(db.Staff, "Id", "Name", staffOnTask.StaffId);
            return View(staffOnTask);
        }

        // GET: StaffOnTasks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffOnTask staffOnTask = await db.StaffOnTask.FindAsync(id);
            if (staffOnTask == null)
            {
                return HttpNotFound();
            }
            return View(staffOnTask);
        }

        // POST: StaffOnTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StaffOnTask staffOnTask = await db.StaffOnTask.FindAsync(id);
            db.StaffOnTask.Remove(staffOnTask);
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
