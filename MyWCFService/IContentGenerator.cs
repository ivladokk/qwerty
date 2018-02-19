using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWCFService
{
    public interface IContentGenerator
    {
        string GetContent();
    }
    public class ProjectsContentGenerator : IContentGenerator
    {
        public string GetContent()
        {
            DBWorker dBWorker = new DBWorker(ConfigurationManager.AppSettings["DBConnectionString"]);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("ProjectID ProjectName Count of users Count of new tasks");
            stringBuilder.Append(Environment.NewLine);
            foreach (var i in dBWorker.GetProjectsReport())
            {
                stringBuilder.AppendFormat("{0} {1} {2} {3}",i.ID,i.ProjectName,i.UsersCount,i.NewTasksCount);
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder.ToString();
        }
    }
}
