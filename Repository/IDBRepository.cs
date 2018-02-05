using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModels;

namespace Repository
{

    public interface IDBRepository
    {
        void Add<T>(T item) where T : class, new();
        void Edit<T>(T item) where T : DBEntity;
        void Delete<T>(T item) where T : DBEntity;
        IEnumerable<T> Read<T>() where T : class, new();
    }

}
