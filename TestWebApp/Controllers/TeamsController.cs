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
    public class TeamsController : Controller
    {
        // GET: Teams
        public ActionResult Index()
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var teams = manager.Select<Team>();
                ViewBag.Teams = teams;
            }
            return View();
        }
        public ActionResult ViewIndex(string alert)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var teams = manager.Select<Team>();
                ViewBag.Teams = teams;
            }
            ViewBag.Alert = alert;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<Team>().FirstOrDefault(x => x.ID == id);
                return View("EditView", item);
            }
        }
        [HttpPost]
        public ActionResult Edit(Team team)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Edit(team);
            }   
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Team team)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Add(team);
            }
            return ViewIndex("Created!");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<Team>().FirstOrDefault(x => x.ID == id);
                manager.Delete(item);
            }
            return ViewIndex("Deleted!");
        }
    }
}