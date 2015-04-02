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
using anthR.Web.Models.arTask;

namespace anthR.Web.Controllers
{
    public class TasksController : Controller
    {

        //private int _completedStatusId = 4;
        private anthRContext _db = new anthRContext();

        // GET: AnthRTasks
        public async Task<ActionResult> Index(int? id, bool? completed, string all)
        {
            
            List<AnthRTask> anthRTask = null;

            if (!completed.HasValue)
            {
                anthRTask = await _db.AnthRTask.Where(t => !id.HasValue 
                    && !t.DateCompleted.HasValue 
                    && (
                            t.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) 
                            || 
                            t.StaffOnTasks.Where(st => st.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Any()
                            || !string.IsNullOrEmpty(all)
                        ) 
                    || t.ProjectId.Equals(id.Value) 
                    && !t.DateCompleted.HasValue 
                    && (
                        t.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) 
                        || 
                        t.StaffOnTasks.Where(st => st.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Any()
                        || !string.IsNullOrEmpty(all)
                    )
                )                
                .OrderBy(p => p.Priority)
                .ThenBy(p => p.Deadline)                
                .ThenBy(p => p.PlannedStart)                
                .Include(a => a.Project).ToListAsync();
            }
            else
            {
                anthRTask = await _db.AnthRTask.Where(t => !id.HasValue && t.DateCompleted.HasValue
                    && (
                        t.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) 
                        || 
                        t.StaffOnTasks.Where(st => st.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Any()
                        || !string.IsNullOrEmpty(all)
                    )
                    || t.ProjectId.Equals(id.Value) 
                    && t.DateCompleted.HasValue
                    && (
                        t.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) 
                        || 
                        t.StaffOnTasks.Where(st => st.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Any()
                        || !string.IsNullOrEmpty(all)
                    )
                )                
                .OrderBy(p => p.Priority)
                .ThenBy(p => p.Deadline)                
                .ThenBy(p => p.PlannedStart)                
                .Include(a => a.Project).ToListAsync();
            }
                
            // if we have a project id, then load project details and assign to breadcrumbextra
            if (id.HasValue)
            {
                
                Project project = null;
                if (anthRTask.Count > 0)
                {
                    project = anthRTask.FirstOrDefault().Project;
                }
                else
                {
                    project = await _db.Project.Where(p => p.Id.Equals(id.Value)).FirstOrDefaultAsync();                
                }
                                
                ViewBag.BreadcrumbExtra = string.Format("- {0} - {1}", project.MasterSite.Name, project.Name);              
                
            }
            else
            {
                ViewBag.BreadcrumbExtra = "- All Tasks";
            }
            
            return View(anthRTask);

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
            var projects = _db.Project.Where(p => !id.HasValue || p.Id.Equals(id.Value)).ToList();
            projects.ForEach(p => { p.Name = p.MasterSite.Name + " - " + p.Name; });

            ViewBag.ProjectId = new SelectList(projects, "Id", "Name");
            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description");
            ViewBag.Priority = new SelectList(Enumerable.Range(1, 4), "Priority");

            return View();

        }

        public ActionResult CreateAjaxForm(int? id)
        {
             // get specific project if we have an id
            var projects = _db.Project.Where(p => !id.HasValue || p.Id.Equals(id.Value)).ToList();
            projects.ForEach(p => { p.Name = p.MasterSite.Name + " - " + p.Name; });

            ViewBag.ProjectId = new SelectList(projects, "Id", "Name");
            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description");
            ViewBag.Priority = new SelectList(Enumerable.Range(1, 4), "Priority");

            return View();
        }

        // POST: AnthRTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProjectId,StatusId,Name,Description,RequestedBy,PlannedStart,Deadline,Priority")] AnthRTask anthRTask)
        {
            if (ModelState.IsValid)
            {

                // add the username
                anthRTask.Username = User.Identity.Name;

                _db.AnthRTask.Add(anthRTask);
                await _db.SaveChangesAsync();
                //return RedirectToAction("Index");

                // redirect to allocate staff to the task
                return RedirectToAction("Create", "StaffOnTasks", new { id = anthRTask.ProjectId, taskId = anthRTask.Id });
                
            }

            ViewBag.ProjectId = new SelectList(_db.Project, "Id", "Name", anthRTask.ProjectId);
            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description", anthRTask.StatusId);
            ViewBag.Priority = new SelectList(Enumerable.Range(1, 4), "Priority");
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
            ViewBag.Priority = new SelectList(Enumerable.Range(1, 4), anthRTask.Priority);

            return View(anthRTask);
        }

        // POST: AnthRTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProjectId,StatusId,Name,Description,RequestedBy,PlannedStart,Deadline,Priority,Username")] AnthRTask anthRTask)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(anthRTask).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(_db.Project, "Id", "Name", anthRTask.ProjectId);
            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description", anthRTask.StatusId);
            ViewBag.Priority = new SelectList(Enumerable.Range(1, 4), "Priority");
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

        public async Task<ActionResult> CompleteAjaxTask(int id)
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
        public async Task<ActionResult> CompleteAjaxTask([Bind(Include = "Id")] AnthRTask anthRTask)
        {
            
            if (ModelState.IsValid)
            {
                var task = _db.AnthRTask.SingleOrDefault(t => t.Id.Equals(anthRTask.Id));
                
                task.DateCompleted = DateTime.Now;
                task.StatusId = _db.Status.Where(s => s.Description.Equals("Complete")).FirstOrDefault().Id;
                
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(_db.Status, "Id", "Description", anthRTask.StatusId);
            return View(anthRTask);

        }
                
        /// <summary>
        /// Shows time allocated to a specific task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> TimeAllocated(int id)
        {

            IQueryable<Timesheet> timesheet = _db.Timesheets.Where(ts => ts.AnthRTaskId.Equals(id));

            return View(await timesheet.ToListAsync());

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
