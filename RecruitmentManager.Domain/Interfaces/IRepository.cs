using RecruitmentManager.Domain.Primitives;
using System.Linq.Expressions;

namespace RecruitmentManager.Domain.Interfaces;

public interface IRepository<TEntity, TId> : IReadRepository<TEntity, TId>, IWriteRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : IEquatable<TId>
{
}