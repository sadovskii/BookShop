using BookShop.Api.Domain.Entities;
using BookShop.Api.Domain.Repositories.Abstract;

namespace BookShop.Api.Domain.Repositories
{
	internal interface IBookRepository : IRepository<Book>
	{
	}
}
