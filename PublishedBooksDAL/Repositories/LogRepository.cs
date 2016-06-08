using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PublishedBooksDAL.Repositories
{
    public class LogRepository<TEntity> : BaseRepo,
        IRepository<TEntity> where
            TEntity : EntityBase
    {
        IRepository<TEntity> innerrepo;

        public LogRepository(IRepository<TEntity> repo)
        {
            innerrepo = repo;
        }

        public bool Insert(TEntity entity)
        {
            Debug.WriteLine("Insertion operation");
            return innerrepo.Insert(entity);
        }

        public bool Update(TEntity entity)
        {
            Debug.WriteLine("Update operation");
            return innerrepo.Update(entity);
        }

        public bool Delete(TEntity entity)
        {
            Debug.WriteLine("Delete operation");
            return innerrepo.Delete(entity);
        }

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            Debug.WriteLine("Search operation");
            return innerrepo.SearchFor(predicate);
        }

        public IList<TEntity> GetAll()
        {
            Debug.WriteLine("Getall operation");
            return innerrepo.GetAll();
        }

        public TEntity GetById(Guid id)
        {
            Debug.WriteLine("GetById operation");
            return innerrepo.GetById(id);
        }
    }
}
