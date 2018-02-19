using DBModels;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TestWebApp.Auth
{
    public interface IUserProvider
    {
        User User { get; set; }
    }
    public class UserIdentity : IIdentity, IUserProvider
    {
        private AuthService _authService;
        public User User { get; set; }
        public IAuthentication Auth { get; set; }
        public User CurrentUser
        {
            get
            {
                return ((IUserProvider)Auth.CurrentUser.Identity).User;
            }
        }

        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.UserName;
                }
                return null;
            }
        }

        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }


        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }
        public UserIdentity(AuthService authService)
        {
            _authService = authService;
        }
        public void Init(string login)
        {
            if (!string.IsNullOrEmpty(login))
            {
                User = _authService.GetUser(login);
            }
        }
        public bool IsInRole(string role)
        {
            return _authService.HasRoles(User, role);
        }
    }
}