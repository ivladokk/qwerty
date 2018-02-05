using DBModels;
using Ninject;
using Repository;
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
        private BugsService bugsService;
        public BugsController()
        {
            bugsService = new BugsService(MvcApplication.AppKernel.Get<DBManager>());
        }
        public ActionResult Index()
        {
            var bugs = bugsService.SelectBugs();
            return View(bugs);
        }
        public ActionResult ViewIndex(string alert)
        {
            var bugs = bugsService.SelectBugs();
            ViewBag.Alert = alert;
            return View("Index", bugs);
        }
        [HttpGet]
        public ActionResult EditBug(int id)
        {
            return PartialView("ModalView", bugsService.GetBugByID(id));
        }

        [HttpGet]
        public ActionResult CreateBug(int id)
        {
            var newBug = new Bug
            {
                ProjectID = id
            };
            return PartialView("ModalView", newBug);
        }

        [HttpPost]
        public ActionResult CreateBug(Bug bug)
        {
            bugsService.AddBug(bug);
            return RedirectToAction("Details", "Projects", new { id = bug.ProjectID });
        }

        [HttpPost]
        public ActionResult EditBug(Bug bug)
        {
            bugsService.EditBug(bug);
            return RedirectToAction("Details", "Projects", new { id = bug.ProjectID });
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("EditView", bugsService.GetBugByID(id));
        }
        [HttpPost]
        public ActionResult Edit(Bug bug)
        {
            bugsService.EditBug(bug);
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Bug bug)
        {
            bugsService.AddBug(bug);
            return ViewIndex("Created!");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            bugsService.DeleteBug(bugsService.GetBugByID(id));
            return ViewIndex("Deleted!");
        }

    }
}