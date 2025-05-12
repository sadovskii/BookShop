using BookShop.Api.Domain.Entities.Abstract;

namespace BookShop.Api.Domain.Repositories.Abstract
{
	public interface IRepository<TEntity>
		where TEntity : Entity
	{
		Task<TEntity?> TryFindByIdAsync(Guid id, CancellationToken cancellationToken = default);

		Task<IEnumerable<TEntity>> GetAllAsync();

		void Add(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);

		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
