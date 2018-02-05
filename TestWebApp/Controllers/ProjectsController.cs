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
    public class ProjectsController : Controller
    {
        // GET: Projects
        private ProjectsService projectsService;
        public ProjectsController()
        {
            projectsService = new ProjectsService(MvcApplication.AppKernel.Get<DBManager>());
        }
        public ActionResult Index()
        {
            var projects = projectsService.SelectProjectsWithCount();
            return View(projects);
        }
        public ActionResult ViewIndex(string alert)
        {
            var projects = projectsService.SelectProjectsWithCount();
            ViewBag.Alert = alert;
            return View("Index", projects);

        }
        public ActionResult Details(int id)
        {
            var details = projectsService.GetProjectDetailViewModel(id);
            return View("DetailsView", details);
        }
        [HttpGet]
        public PartialViewResult EditPartial(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<Project>().FirstOrDefault(x => x.ID == id);
                return PartialView("EditPartialView", item);
            }
            
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
        public ActionResult EditDetails(int id)
        { 
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var task = manager.Select<DBModels.Task>().FirstOrDefault(x => x.ID == id);
                return PartialView("ModalView", task);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }

        [HttpPost]
        public ActionResult Create(Project proj)
        {
            projectsService.CreateNewProject(proj);
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