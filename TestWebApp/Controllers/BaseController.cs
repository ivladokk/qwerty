using DBModels;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApp.Auth;

namespace TestWebApp.Controllers
{
    public class BaseController : Controller
    {
        [Inject]
        public IAuthentication Auth { get; set; }
        
        public User CurrentUser
        {
            get
            {
                return ((IUserProvider)Auth.CurrentUser.Identity).User;
            }
        }
      
    }
}