using AutoMapper;
using BookShop.Api.Domain.Entities.Abstract;
using BookShop.Api.Domain.Repositories.Abstract;
using BookShop.Api.EF.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Api.EF
{
	internal class Repository<TEntity, TDbEntity> : IRepository<TEntity>
		where TEntity : Entity
		where TDbEntity : DbEntity
	{

		protected DbSet<TDbEntity> DbSet { get; }

		protected IMapper Mapper { get; }

		protected DbContext DbContext { get; }



		public Repository(DbContext dbContext, IMapper mapper)
		{
			DbContext = dbContext;
			DbSet = dbContext.Set<TDbEntity>();
			Mapper = mapper;
		}

		public void Add(TEntity entity)
		{
			var dbEntity = Mapper.Map<TDbEntity>(entity);
			DbSet.Add(dbEntity);
		}
		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			var dbEntities = await DbSet.ToListAsync();

			return Mapper.Map<IEnumerable<TEntity>>(dbEntities);
		}

		public async Task<TEntity?> TryFindByIdAsync(Guid id, CancellationToken cancellationToken = default)
		{
			var dbEntity = await DbSet.SingleOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);

			return Mapper.Map<TEntity>(dbEntity);
		}

		public void Update(TEntity entity)
		{
			var dbEntity = DbSet.FirstOrDefault(e => e.Id.Equals(entity.Id));

			if (dbEntity is not null)
			{
				Mapper.Map(entity, dbEntity);
			}
			else
			{
				dbEntity = Mapper.Map<TDbEntity>(entity);
			}

			DbSet.Update(dbEntity);
		}

		public void Delete(TEntity entity)
		{
			var dbEntity = Mapper.Map<TDbEntity>(entity);
			DbSet.Remove(dbEntity);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await DbContext.SaveChangesAsync(cancellationToken);
		}

	}
}
