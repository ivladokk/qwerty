using Models;
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
            var teams = MvcApplication.dbmanager.Select<Team>();
            ViewBag.Teams = teams;
            return View();
        }
        public ActionResult ViewIndex(string alert)
        {
            var Teams = MvcApplication.dbmanager.Select<Team>();
            ViewBag.Teams = Teams;
            ViewBag.Alert = alert;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = MvcApplication.dbmanager.Select<Team>().Where(x => x.ID == id).FirstOrDefault();
            return View("EditView", item);
        }
        [HttpPost]
        public ActionResult Edit(Team team)
        {
            MvcApplication.dbmanager.Edit(team);
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(Team team)
        {
            MvcApplication.dbmanager.Add(team);
            return ViewIndex("Created!");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var item = MvcApplication.dbmanager.Select<Team>().Where(x => x.ID == id).FirstOrDefault();
            MvcApplication.dbmanager.Delete(item);
            return ViewIndex("Deleted!");
        }
    }
}