using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebApp.Controllers
{
    public class TasksController : Controller
    {
        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = MvcApplication.dbmanager.Select<Task>();
            ViewBag.Tasks = tasks;
            return View();
        }
        public ActionResult ViewIndex(string alert)
        {
            var Tasks = MvcApplication.dbmanager.Select<Task>();
            ViewBag.Tasks = Tasks;
            ViewBag.Alert = alert;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = MvcApplication.dbmanager.Select<Task>().Where(x => x.ID == id).FirstOrDefault();
            return View("EditView", item);
        }
        [HttpPost]
        public ActionResult Edit(Task Task)
        {
            MvcApplication.dbmanager.Edit(Task);
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Task Task)
        {
            MvcApplication.dbmanager.Add(Task);
            return ViewIndex("Created!");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var item = MvcApplication.dbmanager.Select<Task>().Where(x => x.ID == id).FirstOrDefault();
            MvcApplication.dbmanager.Delete(item);
            return ViewIndex("Deleted!");
        }

    }
}