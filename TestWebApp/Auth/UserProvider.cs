using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TestWebApp.Auth
{
    public class UserProvider : IPrincipal
    {
        private UserIdentity userIdentity { get; set; }
        //private AuthService _authService;
        #region IPrincipal Members

        public IIdentity Identity
        {
            get
            {
                return userIdentity;
            }
        }

        public bool IsInRole(string role)
        {
            if (userIdentity.User == null)
            {
                return false;
            }
            return userIdentity.IsInRole(role);
        }

        #endregion


        public UserProvider(string name, AuthService authService)
        {
            userIdentity = new UserIdentity(authService);
            userIdentity.Init(name);
        }


        public override string ToString()
        {
            return userIdentity.Name;
        }

    }
}