using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApp.Auth;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class LoginController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var modules = HttpContext.ApplicationInstance.Modules;
            string[] modArray = modules.AllKeys;
            return View(new LoginViewModel());
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel loginView)
        {
          if (ModelState.IsValid)
            {
                var user = Auth.Login(loginView.Login, loginView.Password, loginView.IsPersistent);
                if (user != null)
                {
                    return RedirectToAction("Index", "Projects");
                }
                ModelState["Password"].Errors.Add("Invalid login or password");
            }
            return View(loginView);
        }

        public ActionResult Logout()
        {
            Auth.LogOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult UserLogin()
        {
            return View(CurrentUser);
        }
    }
}