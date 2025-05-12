using BookShop.Api.EF.Entities.Abstract;

namespace BookShop.Api.EF.Repositories.Abstract
{
	public interface IRepository<TEntity>
		where TEntity : DbEntity
	{
		Task<TEntity?> TryFindByIdAsync(Guid id, CancellationToken cancellationToken = default);

		Task<IEnumerable<TEntity>> GetAllAsync();

		void Add(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);

		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
