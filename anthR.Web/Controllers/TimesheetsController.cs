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
using anthR.Web.Models.Task;

namespace anthR.Web.Controllers
{
    public class TimesheetsController : Controller
    {
        private anthRContext db = new anthRContext();

        // GET: Timesheets
        public async Task<ActionResult> Index()
        {
            var timesheets = db.Timesheets.Include(t => t.AnthRTask).Include(t => t.Staff);
            return View(await timesheets.ToListAsync());
        }

        // GET: Timesheets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // GET: Timesheets/Create
        public ActionResult Create(int? id, int? staffId)
        {
            
            // load up task if id is not empty
            var tasks = db.AnthRTask.Where(t => !id.HasValue || t.Id.Equals(id.Value));
            // load staff on task
            var staff = db.Staff.Where(s => s.StaffOnTasks.Where(t => !id.HasValue || t.AnthRTaskId.Equals(id.Value)).Any());
            
            ViewBag.AnthRTask = new SelectList(tasks.ToList(), "Id", "Name", (id.HasValue ? id.Value : 0));
            ViewBag.Staff = new SelectList(staff.ToList(), "Id", "Name", (staffId.HasValue ? staffId.Value : 0));

            return View();

        }

        // POST: Timesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Hours,Mins,HourlyRate,StaffId,AnthRTaskId,DateRecorded")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Timesheets.Add(timesheet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Tasks");
            }

            ViewBag.AnthRTaskId = new SelectList(db.AnthRTask, "Id", "Name", timesheet.AnthRTaskId);
            ViewBag.StaffId = new SelectList(db.Staff, "Id", "Name", timesheet.StaffId);
            return View(timesheet);
        }

        // GET: Timesheets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnthRTaskId = new SelectList(db.AnthRTask, "Id", "Name", timesheet.AnthRTaskId);
            ViewBag.StaffId = new SelectList(db.Staff, "Id", "Name", timesheet.StaffId);
            return View(timesheet);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Hours,Mins,HourlyRate,StaffId,AnthRTaskId,DateRecorded")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AnthRTaskId = new SelectList(db.AnthRTask, "Id", "Name", timesheet.AnthRTaskId);
            ViewBag.StaffId = new SelectList(db.Staff, "Id", "Name", timesheet.StaffId);
            return View(timesheet);
        }

        // GET: Timesheets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: Timesheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            db.Timesheets.Remove(timesheet);
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
