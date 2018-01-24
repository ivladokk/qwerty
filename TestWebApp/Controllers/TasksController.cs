using Models;
using Ninject;
using Services;
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
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var tasks = manager.Select<Task>();
                ViewBag.Tasks = tasks;
            }
                
            return View();
        }
        public ActionResult ViewIndex(string alert)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var tasks = manager.Select<Task>();
                ViewBag.Tasks = tasks;
            }
            ViewBag.Alert = alert;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<Task>().FirstOrDefault(x => x.ID == id);
                return View("EditView", item);
            }
        }
        [HttpPost]
        public ActionResult Edit(Task Task)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Edit(Task);
            }
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Task Task)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Add(Task);
            }
            return ViewIndex("Created!");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<Task>().FirstOrDefault(x => x.ID == id);
                manager.Delete(item);
            }
            return ViewIndex("Deleted!");
        }

    }
}