using Models;
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
            var proj = MvcApplication.dbmanager.Select<Project>();
            ViewBag.Proj = proj;
            return View();
        }
        public ActionResult ViewIndex(string alert)
        {
            var proj = MvcApplication.dbmanager.Select<Project>();
            ViewBag.Proj = proj;
            ViewBag.Alert = alert;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = MvcApplication.dbmanager.Select<Project>().Where(x => x.ID == id).FirstOrDefault();
            return View("EditView", item);
        }
        [HttpPost]
        public ActionResult Edit(Project proj)
        {
            MvcApplication.dbmanager.Edit(proj);
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Project proj)
        {
            MvcApplication.dbmanager.Add(proj);
            return ViewIndex("Created!");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var item = MvcApplication.dbmanager.Select<Project>().Where(x => x.ID == id).FirstOrDefault();
            MvcApplication.dbmanager.Delete(item);
            return ViewIndex("Deleted!");
        }

    }
}