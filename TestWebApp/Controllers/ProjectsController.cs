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
    public class ProjectsController : Controller
    {
        // GET: Projects
        public ActionResult Index()
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var proj = manager.Select<Project>();
                ViewBag.Proj = proj;
            }
            return View();
        }
        public ActionResult ViewIndex(string alert)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var proj = manager.Select<Project>();
                ViewBag.Proj = proj;
            }
            ViewBag.Alert = alert;
            return View("Index");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<Project>().FirstOrDefault(x => x.ID == id);
                return View("EditView", item);
            }
            
        }
        [HttpPost]
        public ActionResult Edit(Project proj)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Edit(proj);
            }
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Project proj)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Add(proj);
            }
            return ViewIndex("Created!");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<Project>().FirstOrDefault(x => x.ID == id);
                manager.Delete(item);
            }
           
            return ViewIndex("Deleted!");
        }

    }
}