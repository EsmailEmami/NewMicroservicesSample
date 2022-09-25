using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Core;

public interface IUnitOfWork : IDisposable
{
    int SaveChanges();
    Task<int> SaveChangesAsync();
    List<string?> SaveAllChanges();
    void Update<TEntity>(TEntity entity) where TEntity : class;
    void Remove<TEntity>(TEntity entity) where TEntity : class;
    void Add<TEntity>(TEntity entity) where TEntity : class;
    void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    void RemoveRange<TEntity>(IEnumerable<TEntity> removeList) where TEntity : class;
    Database Database { get; }
    DbContext Context { get; }
    HttpContext CurrentHttpContext { get; }
}
