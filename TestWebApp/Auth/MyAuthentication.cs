using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DBModels;
using Ninject;
using Repository;
using Services;

namespace TestWebApp.Auth
{
    public class MyAuthentication : IAuthentication
    {
        public HttpContext HttpContext { get; set; }
        private AuthService authService;
        private const string cookieName = "TestWebApp_Cookies";

        public MyAuthentication()
        {
            authService = new AuthService(
                new DBManager(
                    DependencyResolver.Current.GetService<IDBRepository>()));
        }
        private IPrincipal _currentUser;
        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(cookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(ticket.Name, authService);
                        }
                        else
                        {
                            _currentUser = new UserProvider(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        //logger.Error("Failed authentication: " + ex.Message);
                        _currentUser = new UserProvider(null, null);
                    }
                }
                return _currentUser;
            }
        }

        public User Login(string login, string password, bool isPersistent)
        {
            var ret = authService.TryLogin(login, password);
            if (ret != null)
                CreateCookie(login, isPersistent);
            return ret;

        }
        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);
            var encTicket = FormsAuthentication.Encrypt(ticket);
            var AuthCookie = new HttpCookie(cookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            HttpContext.Response.Cookies.Set(AuthCookie);
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[cookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }


        public User Login(string login)
        {
            throw new NotImplementedException();
        }

        
    }
}