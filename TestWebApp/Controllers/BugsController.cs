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
    public class BugsController : Controller
    {
        // GET: Bugs
        public ActionResult Index()
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var bugs = manager.Select<Bug>();
                ViewBag.Bugs = bugs;
            }
            return View();
        }
        public ActionResult ViewIndex(string alert)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var bugs = manager.Select<Bug>();
                ViewBag.Bugs = bugs;
            }
            ViewBag.Alert = alert;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<Bug>().Where(x => x.ID == id).FirstOrDefault();
                return View("EditView", item);
            }
        }
        [HttpPost]
        public ActionResult Edit(Bug bug)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Edit(bug);
            }
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Bug bug)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Add(bug);
            }
            return ViewIndex("Created!");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<Bug>().FirstOrDefault(x => x.ID == id);
                manager.Delete(item);
            }
            return ViewIndex("Deleted!");
        }

    }
}