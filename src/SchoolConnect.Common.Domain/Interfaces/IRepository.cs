using System.Linq.Expressions;
using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Common.Domain.Interfaces;

public interface IRepository<T> where T : AggregateRoot
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    Task<T> AddAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    // Note: GetPagedAsync is available in concrete MongoRepository implementation
    // It's not in this interface to avoid coupling Domain to Application layer
}
