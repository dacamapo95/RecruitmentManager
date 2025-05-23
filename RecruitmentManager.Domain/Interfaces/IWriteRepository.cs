using RecruitmentManager.Domain.Primitives;

namespace RecruitmentManager.Domain.Interfaces;

public interface IWriteRepository<TEntity, TId> 
    where TEntity : Entity<TId>
    where TId : IEquatable<TId>
{
    void Add(TEntity entity);

    void Update(TEntity entity);
    
    void Remove(TEntity entity);
}