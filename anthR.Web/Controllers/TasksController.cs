﻿using System;
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
    public class TasksController : Controller
    {

        //private int _completedStatusId = 4;
        private anthRContext _db = new anthRContext();

        // GET: AnthRTasks
        public async Task<ActionResult> Index(int? id)
        {
            var anthRTask = _db.AnthRTask.Where(t => !id.HasValue && !t.DateCompleted.HasValue || t.ProjectId.Equals(id.Value) && !t.DateCompleted.HasValue)                
                .OrderByDescending(p => p.PlannedStart)
                .ThenByDescending(p => p.Deadline)
                .Include(a => a.Project);
            return View(await anthRTask.ToListAsync());
        }

        // GET: AnthRTasks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnthRTask anthRTask = await _db.AnthRTask.FindAsync(id);
            if (anthRTask == null)
            {
                return HttpNotFound();
            }
            return View(anthRTask);
        }

        // GET: AnthRTasks/Create
        public ActionResult Create(int? id)
        {

            // get specific project if we have an id
            var projects = _db.Project.Where(p => !id.HasValue || p.Id.Equals(id.Value));

            ViewBag.ProjectId = new SelectList(projects.ToList(), "Id", "Name");
            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description");

            return View();

        }

        // POST: AnthRTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProjectId,StatusId,Name,Description,RequestedBy,PlannedStart,Deadline")] AnthRTask anthRTask)
        {
            if (ModelState.IsValid)
            {
                _db.AnthRTask.Add(anthRTask);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(_db.Project, "Id", "Name", anthRTask.ProjectId);
            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description", anthRTask.StatusId);
            return View(anthRTask);
        }

        // GET: AnthRTasks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnthRTask anthRTask = await _db.AnthRTask.FindAsync(id);
            if (anthRTask == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.ProjectId = new SelectList(_db.Project, "Id", "Name", anthRTask.ProjectId);
            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description", anthRTask.StatusId);

            return View(anthRTask);
        }

        // POST: AnthRTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProjectId,StatusId,Name,Description,RequestedBy,PlannedStart,Deadline")] AnthRTask anthRTask)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(anthRTask).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(_db.Project, "Id", "Name", anthRTask.ProjectId);
            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description", anthRTask.StatusId);
            return View(anthRTask);
        }

        // GET: AnthRTasks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnthRTask anthRTask = await _db.AnthRTask.FindAsync(id);
            if (anthRTask == null)
            {
                return HttpNotFound();
            }
            return View(anthRTask);
        }

        // POST: AnthRTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AnthRTask anthRTask = await _db.AnthRTask.FindAsync(id);
            _db.AnthRTask.Remove(anthRTask);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CompleteTask(int id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnthRTask anthRTask = await _db.AnthRTask.FindAsync(id);
            if (anthRTask == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description", anthRTask.StatusId);
            return View(anthRTask);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CompleteTask([Bind(Include = "Id,StatusId,DateCompleted")] AnthRTask anthRTask)
        {
            if (ModelState.IsValid)
            {
                var task = _db.AnthRTask.SingleOrDefault(t => t.Id.Equals(anthRTask.Id));
                
                task.DateCompleted = anthRTask.DateCompleted;
                task.StatusId = anthRTask.StatusId;
                
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description", anthRTask.StatusId);
            return View(anthRTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
