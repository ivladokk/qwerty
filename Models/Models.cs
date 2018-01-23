using MyAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
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

        [SqlField("TeamID", SqlDbType.Int)]
        public int TeamID { get; set; }
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
    }

    [TableName("Bugs")]
    public class Bug : DBEntity
    {
        [SqlField("ProjectID", SqlDbType.Int)]
        public int ProjectID { get; set; }

        [SqlField("TaskID", SqlDbType.Int)]
        public int TaskID { get; set; }

        [SqlField("BugName", SqlDbType.NVarChar)]
        public string BugName { get; set; }

        [SqlField("UserID", SqlDbType.Int)]
        public int UserID { get; set; }
    }

    [TableName("Users")]
    public class User : DBEntity
    {
        [SqlField("UserName", SqlDbType.NVarChar)]
        public string UserName { get; set; }

        [SqlField("TeamID", SqlDbType.Int)]
        public int TeamID { get; set; }
    }

    [TableName("Teams")]
    public class Team : DBEntity
    {
        [SqlField("TeamName", SqlDbType.NVarChar)]
        public string TeamName { get; set; }
    }
}
namespace MyAttributes
{
    public class TableNameAttribute : Attribute
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public TableNameAttribute(string name)
        {
            _name = name;
        }
    }
    public class SqlFieldAttribute : Attribute
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }
        private SqlDbType _sqlDBType;
        public SqlDbType SqlDBType
        {
            get
            {
                return _sqlDBType;
            }
        }

        public SqlFieldAttribute(string name, SqlDbType dbtype)
        {
            _name = name;
            _sqlDBType = dbtype;
        }

    }
}
