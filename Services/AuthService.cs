using DBModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService
    {
        private DBManager _dbManager;
        public AuthService(DBManager dBManager)
        {
            _dbManager = dBManager;
        }
        public User TryLogin(string login,string password)
        {
            return _dbManager.Select<User>().FirstOrDefault(user => user.UserName == login && user.Password == password);
        }
        public User GetUser(string login)
        {
            return _dbManager.Select<User>().FirstOrDefault(user => user.UserName == login);
        }
        public bool HasRoles(User user, string role)
        {
            return _dbManager.Select<Role>().Any(x => x.UserID == user.ID && string.Compare(role, x.RoleName) == 0);
            
        }
    }
}
