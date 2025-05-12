using BookShop.Api.Domain.Entities.Abstract;
using BookShop.Api.Domain.Repositories.Abstract;

namespace BookShop.Api.EF
{
	internal class Repository<TEntity, TDbEntity> : IRepository<TEntity>
		where TEntity : Entity
	{
		public void Add(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public bool HasChanges()
		{
			throw new NotImplementedException();
		}

		public Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity?> TryFindByIdAsync(Guid id, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public void Update(TEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
