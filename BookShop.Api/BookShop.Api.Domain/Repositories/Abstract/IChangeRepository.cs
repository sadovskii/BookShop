using BookShop.Api.Domain.Entities.Abstract;

namespace BookShop.Api.Domain.Repositories.Abstract
{
	public interface IChangeRepository<in TEntity>
		where TEntity : Entity
	{
		void Add(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);

		bool HasChanges();

		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
