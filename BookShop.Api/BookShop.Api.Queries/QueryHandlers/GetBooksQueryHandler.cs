using BookShop.Api.Domain.Entities;
using BookShop.Api.Domain.Repositories;
using BookShop.Api.Queries.Queries;
using MediatR;

namespace BookShop.Api.Queries.QueryHandlers
{
	public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<Book>>
	{
		private readonly IBookRepository _bookRepository;

		public GetBooksQueryHandler(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task<IEnumerable<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
		{
			return await _bookRepository.GetAllAsync();
		}
	}
}
