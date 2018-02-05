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
    public class UsersController : Controller
    {
        private UsersService userService;
        public UsersController()
        {
            userService = new UsersService(MvcApplication.AppKernel.Get<DBManager>());
        }
        public ActionResult Index()
        {
            return View(userService.SelectUsers());
        }

        public ActionResult ViewIndex(string alert)
        {
            ViewBag.Alert = alert;
            return View("Index", userService.SelectUsers());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("EditView", userService.GetUserByID(id));
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            userService.EditUser(user);
            return ViewIndex("Saved!");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("EditView", null);
        }
        public ActionResult Create(User user)
        {
            userService.AddUser(user);
            return ViewIndex("Created!");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            userService.DeleteUser(userService.GetUserByID(id));
            return ViewIndex("Deleted!");
        }
        [HttpGet]
        public ActionResult Assign(int id)
        {
            DBModels.ProjectEmployment item = new ProjectEmployment();
            item.UserID = id;
            return View("AssignView", item);
        }
        [HttpPost]
        public ActionResult Assign(ProjectEmployment prEm)
        {
            userService.AssignUserOnProject(prEm);
            return ViewIndex("Assigned!");
        }


    }
}