using Repository.ADORepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyWCFService
{
    public class Report : IReport
    {
        public IEnumerable<UIModels.ProjectViewModel> BuildProjectsReport()
        {
            var worker = new DBWorker(ConfigurationManager.AppSettings["DBConnectionString"]);
            return worker.GetProjectsReport();
        }
    }
    public class DBWorker
    {
        private Services.ProjectsService service;
        public DBWorker(string constr)
        {
            service = new Services.ProjectsService(
                new Repository.DBManager(
                    new ADORepository(constr)));
        }
        public IEnumerable<UIModels.ProjectViewModel> GetProjectsReport()
        {
            return service.SelectProjectsWithCount();
        }

    }
}
