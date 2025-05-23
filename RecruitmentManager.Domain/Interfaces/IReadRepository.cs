using RecruitmentManager.Domain.Primitives;
using System.Linq.Expressions;

namespace RecruitmentManager.Domain.Interfaces;

public interface IReadRepository<TEntity, TId> 
    where TEntity : Entity<TId>
    where TId : IEquatable<TId>
{
    void DisableTracking();

    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default);
}