using DBModels;
using Ninject;
using Repository;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApp.Filters;

namespace TestWebApp.Controllers
{
    [MyAuth]
    [Authorize(Roles = "User")]
    public class TasksController : Controller
    {
        private TasksService tasksService;
        public TasksController()
        {
            tasksService = new TasksService(
                new DBManager(
                    DependencyResolver.Current.GetService<IDBRepository>()));
        }
        public ActionResult Index()
        {
            var tasks = tasksService.SelectTasks();
            return View(tasks);
        }
        public ActionResult ViewIndex(string alert)
        {
            var tasks = tasksService.SelectTasks();
            ViewBag.Alert = alert;
            return View("Index", tasks);
        }

        [HttpGet]
        public ActionResult EditTask(int id)
        {
            return PartialView("ModalView", tasksService.GetTaskByID(id));
        }

        [HttpGet]
        public ActionResult CreateTask(int id)
        {
            var newTask = new Task
            {
                ProjectID = id
            };
            return PartialView("ModalView", newTask);
        }

        [HttpPost]
        public ActionResult CreateTask(Task task)
        {
            tasksService.AddTask(task);
            return RedirectToAction("Details", "Projects", new { id = task.ProjectID });
        }

        [HttpPost]
        public ActionResult EditTask(Task task)
        {
            tasksService.EditTask(task);
            return RedirectToAction("Details", "Projects", new { id = task.ProjectID });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return PartialView("EditView", tasksService.GetTaskByID(id));
        }
        [HttpPost]
        public ActionResult Edit(Task task)
        {
            tasksService.EditTask(task);
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Task task)
        {
            tasksService.AddTask(task);
            return ViewIndex("Created!");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            tasksService.DeleteTask(tasksService.GetTaskByID(id));
            return ViewIndex("Deleted!");
        }

    }
}