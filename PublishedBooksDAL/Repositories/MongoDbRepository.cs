using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace PublishedBooksDAL.Repositories
{
    public class MongoDbRepository<TEntity> : BaseRepo,
        IRepository<TEntity> where
            TEntity : EntityBase
    {
        private Lazy<MongoDatabase> database;
        private Lazy<MongoCollection<TEntity>> collection;

        public MongoDbRepository()
        {
            database = new Lazy<MongoDatabase>(GetDatabase);
            collection = new Lazy<MongoCollection<TEntity>>(GetCollection);
        }

        public bool Insert(TEntity entity)
        {
            try
            {
                collection.Value.Insert(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            if (entity.Id == null)
                return Insert(entity);

            return collection.Value
                .Save(entity)
                    .DocumentsAffected > 0;
        }

        public bool Delete(TEntity entity) => collection.Value.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate) => collection.Value.AsQueryable<TEntity>().Where(predicate.Compile()).ToList();

        public IList<TEntity> GetAll() => collection.Value.FindAllAs<TEntity>().ToList();

        public TEntity GetById(Guid id) => collection.Value.FindOneByIdAs<TEntity>(id);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (database.IsValueCreated && database.Value != null)
            {
                database.Value.Server.Disconnect();
            }
        }

        #region Private Helper Methods
        private MongoDatabase GetDatabase()
        {
            var client = new MongoClient(GetConnectionString());
            var server = client.GetServer();

            return server.GetDatabase(GetDatabaseName());
        }

        private string GetConnectionString() => ConfigurationManager.AppSettings.Get("MongoDbConnectionString").Replace("{DB_NAME}", GetDatabaseName());

        private string GetDatabaseName() => ConfigurationManager.AppSettings.Get("MongoDbDatabaseName");

        private MongoCollection<TEntity> GetCollection() => database.Value.GetCollection<TEntity>(typeof(TEntity).Name);
        #endregion
    }
}
