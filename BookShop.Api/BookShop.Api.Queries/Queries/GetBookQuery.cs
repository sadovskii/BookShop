using BookShop.Api.Domain.Entities;
using MediatR;

namespace BookShop.Api.Queries.Queries
{
	public class GetBookQuery : IRequest<Book>
	{
		public GetBookQuery(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; }
	}
}
