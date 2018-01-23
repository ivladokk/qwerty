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
    }
}