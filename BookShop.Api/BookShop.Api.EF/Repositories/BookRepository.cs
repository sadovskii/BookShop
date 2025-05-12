using AutoMapper;
using BookShop.Api.EF.Entities;
using BookShop.Api.EF.Repositories.Abstract;

namespace BookShop.Api.EF.Repositories
{
	public class BookRepository : Repository<Book>, IBookRepository
	{
		public BookRepository(BookShopContext dbContext, IMapper mapper) : base(dbContext, mapper)
		{
		}
	}
}
