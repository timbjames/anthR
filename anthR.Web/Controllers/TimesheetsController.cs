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
using anthR.Web.Models.arTask;
using System.Net.Mail;

namespace anthR.Web.Controllers
{
    public class TimesheetsController : Controller
    {
       
        private anthRContext _db = new anthRContext();

        // GET: Timesheets
        public async Task<ActionResult> Index()
        {
            var timesheets = _db.Timesheets.Where(t => t.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Include(t => t.AnthRTask).Include(t => t.Staff);
            return View(await timesheets.ToListAsync());
        }

        // GET: Timesheets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = await _db.Timesheets.FindAsync(id);
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
            var tasks = _db.AnthRTask.Where(t => !id.HasValue || t.Id.Equals(id.Value)).ToList();
            //tasks.ForEach(t => { t.Name = t.Project.MasterSite.Name + " - " + t.Project.Name + " - " + t.Name; });
            // load staff on task
            var staff = _db.Staff.Where(s => s.StaffOnTasks.Where(t => !id.HasValue || t.AnthRTaskId.Equals(id.Value)).Any());
            
            // need to load up the staff id for current username
            if (!staffId.HasValue)
            {
                staffId = staff.Where(s => s.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Id;                
            }

            ViewBag.AnthRTask = new SelectList(tasks, "Id", "Name", (id.HasValue ? id.Value : 0));
            ViewBag.Staff = new SelectList(staff.ToList(), "Id", "Name", (staffId.HasValue ? staffId.Value : 0));
            ViewBag.Project = tasks.FirstOrDefault().Project;

            return View();

        }

        // POST: Timesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Hours,Mins,HourlyRate,StaffId,AnthRTaskId,DateRecorded,Quoted,AlreadyBilled")] Timesheet timesheet, bool? CompleteTask)
        {
            if (ModelState.IsValid)
            {

                // add the username                
                _db.Timesheets.Add(timesheet);

                // if CompleteTask is set and true, then complete the task at this point
                if (CompleteTask.HasValue && CompleteTask.Value)
                {
                    var anthrTask = _db.AnthRTask.Where(at => at.Id.Equals(timesheet.AnthRTaskId)).FirstOrDefault();
                    anthrTask.DateCompleted = DateTime.Now;
                    anthrTask.StatusId = _db.Status.Where(s => s.Description.Equals("Complete")).FirstOrDefault().Id;
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Tasks");

            }

            ViewBag.AnthRTaskId = new SelectList(_db.AnthRTask, "Id", "Name", timesheet.AnthRTaskId);
            ViewBag.StaffId = new SelectList(_db.Staff, "Id", "Name", timesheet.StaffId);
            return View(timesheet);
        }

        // GET: Timesheets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = await _db.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnthRTaskId = new SelectList(_db.AnthRTask, "Id", "Name", timesheet.AnthRTaskId);
            ViewBag.StaffId = new SelectList(_db.Staff, "Id", "Name", timesheet.StaffId);
            return View(timesheet);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Hours,Mins,HourlyRate,StaffId,AnthRTaskId,DateRecorded,Quoted,AlreadyBilled")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(timesheet).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Show", new { month = DateTime.Now.Month });
            }
            ViewBag.AnthRTaskId = new SelectList(_db.AnthRTask, "Id", "Name", timesheet.AnthRTaskId);
            ViewBag.StaffId = new SelectList(_db.Staff, "Id", "Name", timesheet.StaffId);
            return View(timesheet);
        }

        // GET: Timesheets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = await _db.Timesheets.FindAsync(id);
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
            Timesheet timesheet = await _db.Timesheets.FindAsync(id);
            _db.Timesheets.Remove(timesheet);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Show(int? id, int? month, int? year)
        {

            // show the timesheet based on either the month, day or year
            IQueryable<MasterSite> masterSites = null;

            // set the default month value if it is not set
            if (!month.HasValue)
            {
                month = 1;
            }

            // set the default year value if it is not set
            if (!year.HasValue)
            {
                year = DateTime.Now.Year;
            }
            
            // get the days in the month
            int daysInMonth = DateTime.DaysInMonth(year.Value, month.Value);

            // set the start and end dates
            DateTime startDate = new DateTime(year.Value, month.Value, 1);
            DateTime endDate = new DateTime(year.Value, month.Value, daysInMonth, 23,59,59);

            var q = _db.MasterSite
                .Where(ms => ms.Projects.Any(p => p.Tasks.Any(t => t.Timesheet.Any(ts => ts.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)
                            && ts.DateRecorded >= startDate && ts.DateRecorded <= endDate))));

            masterSites = _db.MasterSite
                .Where(ms => ms.Projects.Any(p => p.Tasks.Any(t => t.Timesheet.Any(ts => ts.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) 
                            && ts.DateRecorded >= startDate && ts.DateRecorded <= endDate))));
                        
            ViewBag.StaffId = new SelectList(_db.Staff.ToList(), "Id", "Name");

            return View(await masterSites.ToListAsync());            

        }

        public ActionResult EmailTimesheet(int? id, int? month, int? year, int? email, string me)
        {

            // show the timesheet based on either the month, day or year
            IQueryable<MasterSite> masterSites = null;

            // set the default month if it is not set
            if (!month.HasValue)
            {
                month = 1;
            }

            // set the default year if it is not set
            if (!year.HasValue)
            {
                year = DateTime.Now.Year;
            }
            
            // get the days in the month
            int daysInMonth = DateTime.DaysInMonth(year.Value, month.Value);

            // set the start and end dates
            DateTime startDate = new DateTime(year.Value, month.Value, 1);
            DateTime endDate = new DateTime(year.Value, month.Value, daysInMonth, 23, 59, 59);

            masterSites = _db.MasterSite
                .Where(ms => ms.Projects.Any(p => p.Tasks.Any(t => t.Timesheet.Any(ts => ts.Staff.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase) &&
                            ts.DateRecorded >= startDate && ts.DateRecorded <= endDate))));

            // need to load up the html from the Show action and email it out
            var body = anthR.Utils.IO.Razor.RenderViewToString(this, "Show", masterSites.ToList());

            MailMessage mailMessage = null;
            SmtpClient smtpClient = null;

            // need to get the staff email from the email id
            var staffEmail = _db.Staff.FirstOrDefault(s => s.Id.Equals(email.Value)).Email;
            var myEmail = string.Empty;
           
            // get my email
            myEmail = _db.Staff.FirstOrDefault(s => s.Username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).Email;
                        
            try
            {
                
                mailMessage = new MailMessage();
                mailMessage.To.Add(staffEmail);
                if (!string.IsNullOrEmpty(me) && !string.IsNullOrEmpty(myEmail))
                {
                    mailMessage.Bcc.Add(myEmail);
                }
                mailMessage.From = new MailAddress(myEmail);
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Subject = string.Format("{0} Timesheet", startDate.ToString("MMM yyyy"));

                smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);

            }
            catch (SmtpException smtpEx)
            {
                throw smtpEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Content(body);

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
