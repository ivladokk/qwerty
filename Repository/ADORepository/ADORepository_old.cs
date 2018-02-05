using DBModels;
using MyAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ADORepository_Old
{
    public class ADORepository : IDBRepository
    {
        private SqlConnection connection;
        public ADORepository(string constr)
        {
            int curRety = 0;
            do
            {
                try
                {
                    curRety++;
                    connection = new SqlConnection(constr);
                    connection.Open();
                    break;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    if (curRety > 3)
                        throw new Exception(ex.Message);
                }
            }
            while (true);


        }

        struct ADOFieldInfo
        {
            public string FieldName;
            public SqlDbType Type;
            public object Value;
        }
        public void Add<T>(T item) where T : class, new()
        {
            var tableName = item.GetType().GetCustomAttributes(typeof(TableNameAttribute), true).FirstOrDefault() as TableNameAttribute;
            var props = item.GetType().GetProperties();
            IList<ADOFieldInfo> fields = new List<ADOFieldInfo>();
            foreach (PropertyInfo i in props)
            {
                var fieldAttr = i.GetCustomAttributes(typeof(SqlFieldAttribute), true).FirstOrDefault() as SqlFieldAttribute;
                fields.Add(new ADOFieldInfo
                {
                    FieldName = fieldAttr.Name,
                    Type = fieldAttr.SqlDBType,
                    Value = i.GetValue(item)
                });
            }
            string cmdFields = "";
            string cmdValues = "";
            var lst = fields.Where(x => x.FieldName != "ID").ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                cmdFields += $"{fields[i].FieldName}";
                cmdFields += i != lst.Count - 1 ? "," : string.Empty;

                cmdValues += $"@{fields[i].FieldName}";
                cmdValues += i != lst.Count - 1 ? "," : string.Empty;
            }
            string query = $"insert into {tableName.Name} ({cmdFields}) values ({cmdValues})";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                foreach (var i in lst)
                {
                    cmd.Parameters.Add($"@{i.FieldName}", i.Type, 50).Value = i.Value;
                }
                cmd.ExecuteNonQuery();
            }

        }

        public void Delete<T>(T item) where T : DBEntity
        {
            var tableName = item.GetType().GetCustomAttributes(typeof(TableNameAttribute), true).FirstOrDefault() as TableNameAttribute;
            string query = $"delete from {tableName.Name} where ID={item.ID}";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }

        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public void Edit<T>(T item) where T : DBEntity
        {
            var tableName = item.GetType().GetCustomAttributes(typeof(TableNameAttribute), true).FirstOrDefault() as TableNameAttribute;
            var props = item.GetType().GetProperties();
            IList<ADOFieldInfo> fields = new List<ADOFieldInfo>();
            foreach (PropertyInfo i in props)
            {
                var fieldAttr = i.GetCustomAttributes(typeof(SqlFieldAttribute), true).FirstOrDefault() as SqlFieldAttribute;
                fields.Add(new ADOFieldInfo
                {
                    FieldName = fieldAttr.Name,
                    Type = fieldAttr.SqlDBType,
                    Value = i.GetValue(item)
                });
            }


            string cmdValues = "";
            var lst = fields.Where(x => x.FieldName != "ID").ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                cmdValues += $"{fields[i].FieldName}=@{fields[i].FieldName}";
                cmdValues += i != lst.Count - 1 ? "," : string.Empty;
            }
            string query = $"update {tableName.Name} set {cmdValues} where ID={item.ID}";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                foreach (var i in lst)
                {
                    cmd.Parameters.Add($"@{i.FieldName}", i.Type, 50).Value = i.Value;
                }
                cmd.ExecuteNonQuery();
            }
        }

        private List<T> Deserialize<T>(DataTable dt) where T : class, new()
        {

            List<T> ret = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T obj = new T();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                    propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);

                }
                ret.Add(obj);
            }
            return ret;
        }

        public IEnumerable<T> Read<T>() where T : class, new()
        {
            var tableName = typeof(T).GetCustomAttributes(typeof(TableNameAttribute), true).FirstOrDefault() as TableNameAttribute;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand($"select * from {tableName.Name}", connection)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                SelectCommand = cmd
            };
            adapter.Fill(ds);
            return Deserialize<T>(ds.Tables[0]);

        }

    }
}
