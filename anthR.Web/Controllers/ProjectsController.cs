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

namespace anthR.Web.Controllers
{
    
    public class ProjectsController : Controller
    {
        
        private anthRContext db = new anthRContext();

        // GET: Projects
        public async Task<ActionResult> Index(bool? completed, string all)
        {
            
            IQueryable<Project> project = null;

            if (!completed.HasValue)
            {
                project = db.Project.Where(p => !p.Completed 
                        && (
                            p.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) 
                            || 
                            p.StaffOnProjects.Where(sp => sp.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Any()
                            || !string.IsNullOrEmpty(all)
                        )
                    ).Include(p => p.MasterSite).OrderBy(p => p.MasterSite.Name).ThenBy(p => p.Name);
            }
            else
            {
                project = db.Project.Where(p => p.Completed 
                    && (
                        p.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) 
                        || 
                        p.StaffOnProjects.Where(sp => sp.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Any()
                        || !string.IsNullOrEmpty(all)
                    )
                    ).Include(p => p.MasterSite).OrderBy(p => p.MasterSite.Name).ThenBy(p => p.Name);
            }

            return View(await project.ToListAsync());

        }

        // GET: Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await db.Project.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.MasterSiteId = new SelectList(db.MasterSite, "MasterSiteId", "Name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,MasterSiteId,PlannedStart,Deadline,OnGoing,Quoted")] Project project)
        {
            if (ModelState.IsValid)
            {
                
                // add the username
                project.Username = User.Identity.Name;

                db.Project.Add(project);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");

                //redirect to allocate staff to the project
                return RedirectToAction("Create", "StaffOnProjects", new { id = project.Id });

            }

            ViewBag.MasterSiteId = new SelectList(db.MasterSite, "MasterSiteId", "Name", project.MasterSiteId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await db.Project.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterSiteId = new SelectList(db.MasterSite, "MasterSiteId", "Name", project.MasterSiteId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,MasterSiteId,PlannedStart,Deadline,OnGoing,Username,Quoted")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MasterSiteId = new SelectList(db.MasterSite, "MasterSiteId", "Name", project.MasterSiteId);
            return View(project);
        }

        public async Task<ActionResult> Complete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await db.Project.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            project.DateCompleted = DateTime.Now;

            return View(project);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Complete([Bind(Include = "Id,DateCompleted,AlreadyBilled")] Project model)
        {
            
            Project project = await db.Project.FindAsync(model.Id);
            project.DateCompleted = model.DateCompleted;
            project.AlreadyBilled = model.AlreadyBilled;
            project.Completed = true;
            await db.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        // GET: Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await db.Project.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Project project = await db.Project.FindAsync(id);
            db.Project.Remove(project);
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
