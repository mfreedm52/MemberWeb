using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
       // DbEntityEntry Entry(object o);
        void Dispose();
    }

    public interface IUnitOfWork
    {
        void Dispose();
        void Save();
        void Dispose(bool disposing);
        IRepository<T> Repository<T>() where T : class;
    }

    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);
        void InsertGraph(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Insert(TEntity entity);

        RepositoryQuery<TEntity> Query();
    }
}
