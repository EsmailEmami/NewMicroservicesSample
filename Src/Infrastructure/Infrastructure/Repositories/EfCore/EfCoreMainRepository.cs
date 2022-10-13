using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Application.Core;
using Application.Core.Repositories.EfCore;
using Domain.Core;
using Domain.Core.Events.EfCore;

namespace Infrastructure.Repositories.EfCore;

public class EfCoreMainRepository<TEntity, TPrimaryKey> : EfCoreRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEfCoreEntity<TPrimaryKey>, IAggregateRoot
{
    public virtual DbContext Context { get; }
    public virtual DbSet<TEntity> Table => Context.Set<TEntity>();
    public EfCoreMainRepository(IUnitOfWork dbContextProvider) => Context = (DbContext)dbContextProvider;
    public override IQueryable<TEntity> GetAll() => Table.AsQueryable();

    public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        if (propertySelectors.Length <= 0)
            GetAll();

        var query = GetAll();

        foreach (var propertySelector in propertySelectors)
            query = query.Include(propertySelector);

        return query;
    }

    public override async Task<List<TEntity>> GetAllListAsync() => await GetAll().ToListAsync();

    public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate) => await GetAll().Where(predicate).ToListAsync();

    public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate) => await GetAll().SingleAsync(predicate);

    public override Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id) => Task.FromResult(GetAll().FirstOrDefault(CreateEqualityExpressionForId(id)))!;

    public override Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(GetAll().FirstOrDefault(predicate))!;

    public override TEntity Insert(TEntity entity) => Table.Add(entity).Entity;

    public override Task<TEntity> InsertAsync(TEntity entity) => Task.FromResult(Insert(entity));

    public override TPrimaryKey InsertAndGetId(TEntity entity)
    {
        entity = Insert(entity);

        Context.SaveChanges();

        return entity.Id;
    }

    public override async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        entity = await InsertAsync(entity);

        await Context.SaveChangesAsync();

        return entity.Id;
    }

    public override TEntity Update(TEntity entity)
    {
        AttachIfNot(entity);
        Context.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public override Task<TEntity> UpdateAsync(TEntity entity)
    {
        AttachIfNot(entity);
        Context.Entry(entity).State = EntityState.Modified;
        return Task.FromResult(entity);
    }

    public override void Delete(TEntity entity)
    {
        AttachIfNot(entity);
        Table.Remove(entity);
    }

    public override void Delete(TPrimaryKey id)
    {
        var entity = Table.Local.FirstOrDefault(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id)) ??
                     FirstOrDefault(id);

        Delete(entity);
    }

    public override async Task<int> CountAsync() => await GetAll().CountAsync();

    public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await GetAll().Where(predicate).CountAsync();

    public override async Task<long> LongCountAsync() => await GetAll().LongCountAsync();

    public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) => await GetAll().Where(predicate).LongCountAsync();

    protected virtual void AttachIfNot(TEntity entity)
    {
        if (!Table.Local.Contains(entity))
        {
            Table.Attach(entity);
        }
    }
}

public class EfCoreMainRepository<TEntity> : EfCoreMainRepository<TEntity, int>
    where TEntity : class, IEfCoreEntity<int>, IAggregateRoot
{
    public EfCoreMainRepository(IUnitOfWork dbContextProvider) : base(dbContextProvider)
    {
    }
}