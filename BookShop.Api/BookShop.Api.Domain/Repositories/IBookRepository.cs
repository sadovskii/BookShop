using BookShop.Api.Domain.Entities;
using BookShop.Api.Domain.Repositories.Abstract;

namespace BookShop.Api.Domain.Repositories
{
	public interface IBookRepository : IRepository<Book>
	{
	}
}
