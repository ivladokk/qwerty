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
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var users = manager.Select<User>();
                ViewBag.Users = users;
            }
            return View();
        }

        public ActionResult ViewIndex(string alert)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var Users = manager.Select<User>();
                ViewBag.Users = Users;
            }
            ViewBag.Alert = alert;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<User>().FirstOrDefault(x => x.ID == id);
                return View("EditView", item);
            }
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Edit(user);
            }
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(User user)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                manager.Add(user);
            }
            return ViewIndex("Created!");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var manager = MvcApplication.AppKernel.Get<DBManager>())
            {
                var item = manager.Select<User>().FirstOrDefault(x => x.ID == id);
                manager.Delete(item);
            }
            return ViewIndex("Deleted!");
        }
    }
}