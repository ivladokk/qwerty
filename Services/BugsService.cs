using DBModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BugsService
    {
        private DBManager _dbManager;
        public BugsService(DBManager dbManager)
        {
            _dbManager = dbManager;
        }
        public IEnumerable<Bug> SelectBugs()
        {
            return _dbManager.Select<Bug>();
        }

        public Bug GetBugByID(int id)
        {
            return _dbManager.Select<Bug>().FirstOrDefault(b => b.ID == id);
        }
        public void AddBug(Bug bug)
        {
            _dbManager.Add(bug);
        }
        public void EditBug(Bug bug)
        {
            _dbManager.Edit(bug);
        }
        public void DeleteBug(Bug bug)
        {
            _dbManager.Delete(bug);
        }
    }
}
