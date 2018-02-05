using DBModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TasksService
    {
        private DBManager _dbManager;
        public TasksService(DBManager dbManager)
        {
            _dbManager = dbManager;
        }
        public IEnumerable<DBModels.Task> SelectTasks()
        {
            return _dbManager.Select<DBModels.Task>();
        }

        public DBModels.Task GetTaskByID(int id)
        {
            return _dbManager.Select<DBModels.Task>().FirstOrDefault(t => t.ID == id);
        }
        public void AddTask(DBModels.Task task)
        {
            _dbManager.Add(task);
        }
        public void EditTask(DBModels.Task task)
        {
            _dbManager.Edit(task);
        }
        public void DeleteTask(DBModels.Task task)
        {
            _dbManager.Delete(task);
        }
    }
}
