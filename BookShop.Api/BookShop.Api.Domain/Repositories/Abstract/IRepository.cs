using BookShop.Api.Domain.Entities.Abstract;

namespace BookShop.Api.Domain.Repositories.Abstract
{
	public interface IRepository<TEntity> : IQueryRepository<TEntity>, IChangeRepository<TEntity>
		where TEntity : Entity
	{
	}
}
