using AutoMapper;
using BookShop.Api.EF.Entities.Abstract;
using BookShop.Api.EF.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Api.EF
{
	public class Repository<TEntity> : IRepository<TEntity>
		where TEntity : DbEntity
	{

		protected DbSet<TEntity> DbSet { get; }

		protected IMapper Mapper { get; }

		protected BookShopContext DbContext { get; }



		public Repository(BookShopContext dbContext, IMapper mapper)
		{
			DbContext = dbContext;
			DbSet = dbContext.Set<TEntity>();
			Mapper = mapper;
		}

		public void Add(TEntity entity)
		{
			DbSet.Add(entity);
		}
		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await DbSet.ToListAsync();
		}

		public async Task<TEntity?> TryFindByIdAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return await DbSet.SingleOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);
		}

		public void Update(TEntity entity)
		{
			DbSet.Update(entity);
		}

		public void Delete(TEntity entity)
		{
			DbSet.Remove(entity);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await DbContext.SaveChangesAsync(cancellationToken);
		}

	}
}
