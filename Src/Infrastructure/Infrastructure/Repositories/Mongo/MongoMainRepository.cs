using Application.Core.MongoOptions;
using Application.Core.Repositories.Mongo;
using Application.Extensions;
using Domain.Core.Entities;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Mongo;

public class MongoMainRepository<TEntity, TPrimaryKey> : MongoRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
{
    private readonly IMongoCollection<TEntity> _collection;

    public MongoMainRepository(IMongoDbSettings options)
    {
        var database = new MongoClient(options.ConnectionString).GetDatabase(options.DatabaseName);
        _collection = database.GetCollection<TEntity>(MongoExtensions.GetCollectionName(typeof(TEntity)));
    }

    public override IQueryable<TEntity> GetAll() => _collection.AsQueryable();

    public override TEntity Insert(TEntity entity)
    {
        _collection.InsertOne(entity);
        return entity;
    }

    public override TEntity Update(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
        _collection.FindOneAndReplace(filter, entity);
        return entity;
    }

    public override void Delete(TEntity entity) => Delete(entity.Id);

    public override void Delete(TPrimaryKey id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));

        var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);
        _collection.FindOneAndDelete(filter);
    }
}

public class MongoMainRepository<TEntity> : MongoMainRepository<TEntity, int>
    where TEntity : class, IEntity<int>
{
    public MongoMainRepository(IMongoDbSettings options) : base(options)
    {
    }
}