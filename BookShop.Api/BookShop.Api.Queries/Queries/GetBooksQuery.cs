using BookShop.Api.Domain.Entities;
using MediatR;

namespace BookShop.Api.Queries.Queries
{
	public class GetBooksQuery : IRequest<IEnumerable<Book>>
	{
	}
}
