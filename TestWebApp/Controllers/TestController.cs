using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            var proj = MvcApplication.dbmanager.Select<Project>();
            ViewBag.Proj = proj;
            return View();
        }

        public IEnumerable<Project> GetProjects()
        {
            return MvcApplication.dbmanager.Select<Project>();
        }
        public IEnumerable<Task> GetTasks()
        {
            return MvcApplication.dbmanager.Select<Task>();
        }
        public IEnumerable<User> GetUsers()
        {
            return MvcApplication.dbmanager.Select<User>();
        }
        public IEnumerable<Bug> GetBugs()
        {
            return MvcApplication.dbmanager.Select<Bug>();
        }
        public IEnumerable<Team> GetTeams()
        {
            return MvcApplication.dbmanager.Select<Team>();
        }

        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
