using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApp.Filters;

namespace TestWebApp.Controllers
{
    [MyAuth]
    [Authorize(Roles = "Admin")]
    public class ReportingController : BaseController
    {
        // GET: Reporting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MsgIndex(string message)
        {
            ViewBag.Message = message;
            return View("Index");
        }
        public ActionResult ProjectsReport()
        { 
            using (var client = new MyReportingService.ReportClient())
            {
                var data = client.BuildProjectsReport();
                return View("ProjectsReportView", data);
            }
        }
        public ActionResult SendProjectsReportToEmail()
        {
            if (CurrentUser != null)
            {
                var client = new EmailReportService.EmailReportClient();
                client.SendReport(CurrentUser.Email);
                return MsgIndex("Report was sended");
            }
            else return MsgIndex("Error");
        }

    }
}