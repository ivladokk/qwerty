using DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository.EFRepository
{
    public class EFRepository:IDBRepository
    {
        private readonly string _constr;
        public EFRepository(string constr)
        {
            _constr = constr;
        }

        public void Add<T>(T item) where T : class, new()
        {
            using (var _efcontext = new EFContext(_constr))
            {
                var dbset = _efcontext.Set<T>();
                dbset.Add(item);
                _efcontext.SaveChanges();
            }
        }
            
        public void Delete<T>(T item) where T : DBEntity
        {
            using (var _efcontext = new EFContext(_constr))
            {
                var dbset = _efcontext.Set<T>();
                dbset.Remove(item);
                _efcontext.SaveChanges();
            } 
        }
        public void Edit<T>(T item) where T : DBEntity
        {
            using (var _efcontext = new EFContext(_constr))
            {
                _efcontext.Set<T>().AddOrUpdate(item);
                _efcontext.SaveChanges();
            }
            
        }
        public IEnumerable<T> Read<T>() where T : class, new()
        {
            using (var _efcontext = new EFContext(_constr))
            {
                var dbset = _efcontext.Set<T>().ToList();
                return dbset;
            }
            
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
        public DbSet<DBModels.Project> Projects { get; set; }
        public DbSet<DBModels.Task> Tasks { get; set; }
        public DbSet<DBModels.Bug> Bugs { get; set; }
        public DbSet<DBModels.User> Users { get; set; }
        public DbSet<DBModels.ProjectEmployment> ProjectEmployments { get; set; }
    }
}
