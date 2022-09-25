using Domain.Core.Entities;
using Domain.Exceptions;
using System.Linq.Expressions;

namespace Application.Core.Repositories.EfCore;

public abstract class EfCoreRepository<TEntity, TPrimaryKey> : IEfCoreRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    public abstract IQueryable<TEntity> GetAll();

    public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors) => GetAll();

    public virtual bool Exists(TPrimaryKey Id) => GetAll().Any(CreateEqualityExpressionForId(Id));

    public virtual List<TEntity> GetAllList() => GetAll().ToList();

    public virtual Task<List<TEntity>> GetAllListAsync() => Task.FromResult(GetAllList());

    public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate) => GetAll().Where(predicate).ToList();

    public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(GetAllList(predicate));

    public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod) => queryMethod(GetAll());

    public virtual TEntity Get(TPrimaryKey id)
    {
        var entity = FirstOrDefault(id);
        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id);
        }

        return entity;
    }

    public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
    {
        var entity = await FirstOrDefaultAsync(id);
        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id);
        }

        return entity;
    }

    public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate) => GetAll().Single(predicate);

    public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(Single(predicate));

    public virtual TEntity FirstOrDefault(TPrimaryKey id) => GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));

    public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id) => Task.FromResult(FirstOrDefault(id));

    public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => GetAll().FirstOrDefault(predicate);

    public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(FirstOrDefault(predicate));

    public virtual TEntity Load(TPrimaryKey id) => Get(id);

    public abstract TEntity Insert(TEntity entity);

    public virtual Task<TEntity> InsertAsync(TEntity entity) => Task.FromResult(Insert(entity));

    public virtual TPrimaryKey InsertAndGetId(TEntity entity) => Insert(entity).Id;

    public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity) => Task.FromResult(InsertAndGetId(entity));

    public virtual TEntity InsertOrUpdate(TEntity entity) => entity.IsTransient() ? Insert(entity) : Update(entity);

    public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity) => entity.IsTransient() ? await InsertAsync(entity) : await UpdateAsync(entity);

    public virtual TPrimaryKey InsertOrUpdateAndGetId(TEntity entity) => InsertOrUpdate(entity).Id;

    public virtual Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity) => Task.FromResult(InsertOrUpdateAndGetId(entity));

    public abstract TEntity Update(TEntity entity);

    public virtual Task<TEntity> UpdateAsync(TEntity entity) => Task.FromResult(Update(entity));

    public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
    {
        var entity = Get(id);
        updateAction(entity);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
    {
        var entity = await GetAsync(id);
        await updateAction(entity);
        return entity;
    }

    public abstract void Delete(TEntity entity);
    public abstract void Delete(TPrimaryKey id);
    public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        foreach (var entity in GetAll().Where(predicate).ToList())
            Delete(entity);
    }

    public virtual int Count() => GetAll().Count();

    public virtual Task<int> CountAsync() => Task.FromResult(Count());

    public virtual int Count(Expression<Func<TEntity, bool>> predicate) => GetAll().Where(predicate).Count();

    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(Count(predicate));

    public virtual long LongCount() => GetAll().LongCount();

    public virtual Task<long> LongCountAsync() => Task.FromResult(LongCount());

    public virtual long LongCount(Expression<Func<TEntity, bool>> predicate) => GetAll().Where(predicate).LongCount();

    public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(LongCount(predicate));

    protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    {
        var lambdaParam = Expression.Parameter(typeof(TEntity));

        var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

        Expression<Func<object>> closure = () => id;
        var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

        var lambdaBody = Expression.Equal(leftExpression, rightExpression);

        return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
    }
}

public abstract class EfCoreRepository<TEntity> : EfCoreRepository<TEntity, int> where TEntity : class, IEntity<int>
{
}
