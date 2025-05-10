using BookShop.Api.Domain.Entities;

namespace BookShop.Api.Domain.Repositories.Abstract
{
	public interface IQueryRepository<TEntity>
		where TEntity : Entity
	{
		Task<TEntity?> TryFindByIdAsync(Guid id, CancellationToken cancellationToken = default);

		Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

		Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
	}
}
