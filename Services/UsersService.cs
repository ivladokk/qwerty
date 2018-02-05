using DBModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UsersService
    {
        private DBManager _dbManager;
        public UsersService(DBManager dbManager)
        {
            _dbManager = dbManager;
        }
        public IEnumerable<User> SelectUsers()
        {
            return _dbManager.Select<User>();
        }

        public User GetUserByID(int id)
        {
            return _dbManager.Select<User>().FirstOrDefault(u => u.ID == id);
        }
        public void AddUser(User user)
        {
            _dbManager.Add(user);
        }
        public void EditUser(User user)
        {
            _dbManager.Edit(user);
        }
        public void DeleteUser(User user)
        {
            _dbManager.Delete(user);
        }
        public void AssignUserOnProject(ProjectEmployment prEm)
        {
            _dbManager.Add(prEm);
        }
    }
}
