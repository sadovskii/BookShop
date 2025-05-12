using BookShop.Api.Domain.Entities;
using BookShop.Api.Domain.Repositories;
using BookShop.Api.Queries.Queries;
using MediatR;

namespace BookShop.Api.Queries.QueryHandlers
{
	public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Book?>
	{
		private readonly IBookRepository _bookRepository;

		public GetBookQueryHandler(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task<Book?> Handle(GetBookQuery request, CancellationToken cancellationToken)
		{
			return await _bookRepository.TryFindByIdAsync(request.Id);
		}
	}
}
