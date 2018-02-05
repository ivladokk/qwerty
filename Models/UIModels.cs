using DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIModels
{
    public class ProjectViewModel
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
        public int UsersCount { get; set; }
        public int NewTasksCount { get; set; }
    }

    public class ProjectDetailViewModel
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
        public List<ProjectUserViewModel> Users { get; set; }
        public List<ProjectTaskViewModel> Tasks{ get; set; }
        public List<ProjectBugViewModel> Bugs { get; set; }
    }
    public class ProjectUserViewModel
    {
        public User User { get; set; }
        public UserRole Role { get; set; }
    }
    public class ProjectTaskViewModel
    {
        public DBModels.Task Task { get; set; }
        public User User { get; set; }
    }
    public class ProjectBugViewModel
    {
        public Bug Bug { get; set; }
        public User User { get; set; }
    }
}
