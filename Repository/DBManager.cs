using DBModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DBManager:IDisposable
    {
        private IDBRepository _repository;
        public int retryCount = 3;
        public DBManager(IDBRepository repository)
        {

            _repository = repository;
        }

        public void Add<T>(T item) where T : class, new()
        {
            int curRetry = 0;
            do
            {
                try
                {
                    curRetry++;
                    _repository.Add(item);
                    break;

                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    if (curRetry >= retryCount)
                        throw new Exception(ex.Message);
                }
            }
            while (true);


        }
        public void Edit<T>(T item) where T : DBEntity
        {
            int curRetry = 0;
            do
            {
                try
                {
                    curRetry++;
                    _repository.Edit(item);
                    break;

                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    if (curRetry >= retryCount)
                        throw new Exception(ex.Message);
                }
            }
            while (true);

        }
        public void Delete<T>(T item) where T : DBEntity
        {
            int curRetry = 0;
            do
            {
                try
                {
                    curRetry++;
                    _repository.Delete(item);
                    break;

                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    if (curRetry >= retryCount)
                        throw new Exception(ex.Message);
                }
            }
            while (true);

        }
        public IEnumerable<T> Select<T>() where T : class, new()
        {
            int curRetry = 0;
            do
            {
                try
                {
                    curRetry++;
                    return _repository.Read<T>();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    if (curRetry >= retryCount)
                        throw new Exception(ex.Message);
                }
            }
            while (true);


        }

        public void Dispose()
        {
            
        }
    }
}
