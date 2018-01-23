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

        public ActionResult Edit(int id)
        {

           
            return View("~/EditView");
        }
    }
}