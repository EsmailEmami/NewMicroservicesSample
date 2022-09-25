using System.ComponentModel.DataAnnotations;
using Application.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Context;

public class EfCoreMainDbContext<T> : DbContext, IUnitOfWork where T : DbContext
{
    public EfCoreMainDbContext(DbContextOptions<T> options) : base(options)
    {
    }

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    public new void Update<TEntity>(TEntity entity) where TEntity : class => Context.Update(entity);
    public new void Remove<TEntity>(TEntity entity) where TEntity : class => Context.Remove(entity);
    public new void Add<TEntity>(TEntity entity) where TEntity : class => Context.Add(entity);

    public List<string?> SaveAllChanges()
    {
        var entities = from entry in ChangeTracker.Entries()
                       where entry.State is EntityState.Modified or EntityState.Added
                       select entry.Entity;

        var validationResults = new List<ValidationResult>();
        foreach (var entity in entities)
        {
            if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
            {
                return validationResults.Select(x => x.ErrorMessage).ToList();
            }
        }

        base.SaveChanges();

        return new List<string?>();
    }

    public void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class =>
        Context.AddRange(entities);

    public void RemoveRange<TEntity>(IEnumerable<TEntity> removeList) where TEntity : class =>
        Context.RemoveRange(removeList);

    public new Database Database => Database;
    public DbContext Context => this;
    public HttpContext CurrentHttpContext => CurrentHttpContext;
}