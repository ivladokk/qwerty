using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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