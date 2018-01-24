using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebApp.Controllers
{
    public class BugsController : Controller
    {
        // GET: Bugs
        public ActionResult Index()
        {
            var bugs = MvcApplication.dbmanager.Select<Bug>();
            ViewBag.Bugs = bugs;
            return View();
        }
        public ActionResult ViewIndex(string alert)
        {
            var bugs = MvcApplication.dbmanager.Select<Bug>();
            ViewBag.Bugs = bugs;
            ViewBag.Alert = alert;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = MvcApplication.dbmanager.Select<Bug>().Where(x => x.ID == id).FirstOrDefault();
            return View("EditView", item);
        }
        [HttpPost]
        public ActionResult Edit(Bug bug)
        {
            MvcApplication.dbmanager.Edit(bug);
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Bug bug)
        {
            MvcApplication.dbmanager.Add(bug);
            return ViewIndex("Created!");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var item = MvcApplication.dbmanager.Select<Bug>().Where(x => x.ID == id).FirstOrDefault();
            MvcApplication.dbmanager.Delete(item);
            return ViewIndex("Deleted!");
        }

    }
}