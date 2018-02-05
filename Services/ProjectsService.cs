using DBModels;
using UIModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Services
{
    
    public class ProjectsService
    {
        private DBManager _dbManager;
        public ProjectsService(DBManager dbManager)
        {
            _dbManager = dbManager;       

        }
        public IEnumerable<DBModels.Project> SelectProjects()
        {
            return _dbManager.Select<DBModels.Project>();
        }
        public IEnumerable<ProjectViewModel> SelectProjectsWithCount()
        {            

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectViewModel>()
               .ForMember("UsersCount", opt => opt.MapFrom(proj => GetProjectUsersCount(proj.ID)))
               .ForMember("NewTasksCount", opt => opt.MapFrom(proj => GetNewTasksCount(proj.ID))));
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<Project>, List<ProjectViewModel>>(SelectProjects());

            //return Mapper.Map<IEnumerable<Project>, List<ProjectViewModel>>(SelectProjects());

        }
        public int GetProjectUsersCount(int projectId)
        {
            var ret = _dbManager.Select<ProjectEmployment>().Where(proj => proj.ProjectID == projectId).Count();
            return ret;
        }
        public int GetNewTasksCount(int projectId)
        {
            return _dbManager.Select<DBModels.Task>().Where(task => task.ProjectID == projectId && task.Status == Status.New).Count();
        }

        public ProjectDetailViewModel GetProjectDetailViewModel(int projectId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDetailViewModel>()
                .ForMember("Users", opt => opt.MapFrom(proj => GetProjectUsers(proj.ID)))
                .ForMember("Tasks", opt => opt.MapFrom(proj => GetProjectTasks(proj.ID)))
                .ForMember("Bugs", opt => opt.MapFrom(proj => GetProjectBugs(proj.ID))));
            var mapper = config.CreateMapper();
            var project = _dbManager.Select<Project>().FirstOrDefault(proj => proj.ID == projectId);
            return mapper.Map<Project, ProjectDetailViewModel>(project);
        }
        public List<ProjectUserViewModel> GetProjectUsers(int projectId)
        {
            return _dbManager.Select<ProjectEmployment>().Where(emp => emp.ProjectID == projectId)
                .Join(_dbManager.Select<User>(), prEmp => prEmp.UserID, user => user.ID,
                (prEmp, user) => new ProjectUserViewModel
                {
                    User = user,
                    Role = prEmp.Role
                })
                .ToList();
        }
        public List<ProjectTaskViewModel> GetProjectTasks(int projectId)
        {
            return _dbManager.Select<DBModels.Task>().Where(t => t.ProjectID == projectId)
                .Join(_dbManager.Select<User>(), task => task.UserID, user => user.ID,
                (task, user) => new ProjectTaskViewModel
                {
                    User = user,
                    Task = task
                })
                .ToList();
        }
        public List<ProjectBugViewModel> GetProjectBugs(int projectId)
        {
            return _dbManager.Select<DBModels.Bug>().Where(b => b.ProjectID == projectId)
                .Join(_dbManager.Select<User>(), bug => bug.UserID, user => user.ID,
                (bug, user) => new ProjectBugViewModel
                {
                    User = user,
                    Bug = bug
                })
                .ToList();
        }
        public void CreateNewProject(Project project)
        {
            _dbManager.Add(project);
        }


    }
}
