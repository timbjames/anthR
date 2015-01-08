using anthR.Web.Models.Core;
using anthR.Web.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace anthR.Web.Controllers
{
    public class ToDoController : Controller
    {

        private readonly anthRContext _db;

        public ToDoController()
        {
            _db = new anthRContext();
        }

        // GET: Todo
        public ActionResult Index()
        {
            var todos = _db.TodoItems.Where(td => !td.IsDone).OrderBy(td => td.Priority).ThenBy(td => td.Id).ToList();
            return View(todos);
        }

        public ActionResult Details(int id)
        {

            var todo = _db.TodoItems.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);

        }

        public ActionResult Create()
        {
            ViewBag.Priority = new SelectList(Enumerable.Range(1, 4), "Priority");
            ViewBag.Status = new SelectList(_db.Status, "Id", "Description");
            return View(new TodoItemEditModel() { MasterSites = _db.MasterSite.ToList() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoItemEditModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var todo = new TodoItem()
            {
                Title = model.Title,
                IsDone = model.IsDone,
                Priority = model.Priority,
                Description = model.Description,
                Deadline = model.Deadline,
                MasterSiteId = model.MastersiteId
            };

            _db.TodoItems.Add(todo);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            
            var todo = _db.TodoItems.SingleOrDefault(x => x.Id.Equals(id));
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TodoItemEditModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var todo = _db.TodoItems.SingleOrDefault(x => x.Id.Equals(id));
            if (todo == null)
            {
                return HttpNotFound();
            }

            todo.Title = model.Title;
            todo.Description = model.Description;
            todo.Deadline = model.Deadline;
            todo.IsDone = model.IsDone;

            _db.SaveChanges();

            return RedirectToAction("Index", new { id = id });

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = _db.TodoItems.SingleOrDefault(x => x.Id == id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodoItem todoItem = _db.TodoItems.SingleOrDefault(x => x.Id.Equals(id));
            _db.TodoItems.Remove(todoItem);
            _db.SaveChanges();
            return RedirectToAction("Index");
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