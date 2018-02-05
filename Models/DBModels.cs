using MyAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DBModels
{
    public abstract class DBEntity
    {
        [SqlField("ID",SqlDbType.Int)]
        public int ID { get; set; }
    }

    [TableName("Projects")]
    public class Project : DBEntity
    {
        [SqlField("ProjectName", SqlDbType.NVarChar)]
        public string ProjectName { get; set; }

        [SqlField("Description", SqlDbType.NVarChar)]
        public string Description { get; set; }

        [SqlField("Customer", SqlDbType.NVarChar)]
        public string Customer { get; set; }
    }
    [TableName("Tasks")]
    public class Task : DBEntity
    {
        [SqlField("ProjectID", SqlDbType.Int)]
        public int ProjectID { get; set; }

        [SqlField("TaskName", SqlDbType.NVarChar)]
        public string TaskName { get; set; }

        [SqlField("UserID", SqlDbType.Int)]
        public int UserID { get; set; }

        [SqlField("Status", SqlDbType.Int)]
        public Status Status { get; set; }
    }

    [TableName("Bugs")]
    public class Bug : DBEntity
    {
        [SqlField("ProjectID", SqlDbType.Int)]
        public int ProjectID { get; set; }

        [SqlField("BugName", SqlDbType.NVarChar)]
        public string BugName { get; set; }

        [SqlField("Status", SqlDbType.Int)]
        public Status Status { get; set; }

        [SqlField("UserID", SqlDbType.Int)]
        public int UserID { get; set; }
    }

    [TableName("Users")]
    public class User : DBEntity
    {
        [SqlField("UserName", SqlDbType.NVarChar)]
        public string UserName { get; set; }

    }

    [TableName("ProjectEmployments")]
    public class ProjectEmployment : DBEntity
    {
        [SqlField("ProjectID", SqlDbType.Int)]
        public int ProjectID { get; set; }

        [SqlField("UserID", SqlDbType.Int)]
        public int UserID { get; set; }

        [SqlField("Role", SqlDbType.Int)]
        public UserRole Role { get; set; }

    }
    public enum UserRole
    {
        Manager = 1,
        QA,
        BA,
        Developer
    }
    public enum Status
    {
        New = 1,
        InProgress,
        ReadyToTest,
        Done
    }
}