using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Models;
using Repository;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using MyAttributes;
using System.Collections;
using System.Data.Entity.Migrations;

namespace Services
{
    public class DBManager: IDisposable
    {
        private IDBRepository _repository;
        public DBManager(IDBRepository repository)
        {
            _repository = repository;
        }
        public void Add<T>(T item) where T : class, new()
        {
            try
            {
                _repository.Add(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public void Edit<T>(T item) where T : DBEntity
        {
            try
            {
                _repository.Edit(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public void Delete<T>(T item) where T : DBEntity
        {
            try
            {
                _repository.Delete(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public IEnumerable<T> Select<T>() where T : class, new()
        {
            try
            {
                return _repository.Read<T>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public void Dispose()
        {
            _repository.Dispose();
        }

    }

    public class EFRepository : IDBRepository
    {
        private EFContext _efcontext;
        public EFRepository(string constr)
        {
            _efcontext = new EFContext(constr);
        }

        public void Add<T>(T item) where T : class, new()
        {
            var dbset = _efcontext.Set<T>();
            dbset.Add(item);
            _efcontext.SaveChanges();

        }

        public void Delete<T>(T item) where T : DBEntity
        {
            var dbset = _efcontext.Set<T>();
            dbset.Remove(item);
            _efcontext.SaveChanges();
        }

        
        public void Dispose()
        {
            _efcontext.Dispose();
        }

        public void Edit<T>(T item) where T : DBEntity
        {
            _efcontext.Set<T>().AddOrUpdate(item);
            _efcontext.SaveChanges();
        }
        public IEnumerable<T> Read<T>() where T : class, new()
        {
            var dbset = _efcontext.Set<T>().ToList();
            return dbset;
        }
    }

    class EFContext : DbContext
    {
        public EFContext(string nameOrConnectionString) :
            base(nameOrConnectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EFContext>(null);
            
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Models.Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
    public class ADORepository : IDBRepository
    {
        private SqlConnection connection;
        public ADORepository(string constr)
        {
            connection = new SqlConnection(constr);
            connection.Open();
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
            for (int i = 0; i<lst.Count; i++)
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
