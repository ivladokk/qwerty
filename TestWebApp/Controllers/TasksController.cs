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
    }
}