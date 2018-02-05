using UIModels;
using Ninject;
using Repository;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebApp.Helpers
{
    public static class ProjectsHelper
    {
        public static IEnumerable<SelectListItem> GetProjectUsers(int projectId)
        {
            var service = new ProjectsService(MvcApplication.AppKernel.Get<DBManager>());
            return service.GetProjectUsers(projectId).Select(x => new SelectListItem
            {
                Text = x.User.UserName,
                Value = x.User.ID.ToString()
            });
        }
        public static IEnumerable<SelectListItem> GetProjects()
        {
            var service = new ProjectsService(MvcApplication.AppKernel.Get<DBManager>());
            return service.SelectProjects().Select(x => new SelectListItem
            {
                Text = x.ProjectName,
                Value = x.ID.ToString()
            });
        }
    }
}