using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebApp.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            var users = MvcApplication.dbmanager.Select<User>();
            ViewBag.Users = users;
            return View();
        }

        public ActionResult ViewIndex(string alert)
        {
            var Users = MvcApplication.dbmanager.Select<User>();
            ViewBag.Users = Users;
            ViewBag.Alert = alert;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = MvcApplication.dbmanager.Select<User>().Where(x => x.ID == id).FirstOrDefault();
            return View("EditView", item);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            MvcApplication.dbmanager.Edit(user);
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(User user)
        {
            MvcApplication.dbmanager.Add(user);
            return ViewIndex("Created!");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var item = MvcApplication.dbmanager.Select<User>().Where(x => x.ID == id).FirstOrDefault();
            MvcApplication.dbmanager.Delete(item);
            return ViewIndex("Deleted!");
        }
    }
}