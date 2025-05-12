using BookShop.Api.Application.Commands;
using BookShop.Api.Domain.Repositories;
using MediatR;

namespace BookShop.Api.Application.CommandHandlers
{
	internal class RemoveBookCommandHandler : IRequestHandler<RemoveBookCommand>
	{
		private readonly IBookRepository _bookRepository;

		public RemoveBookCommandHandler(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task Handle(RemoveBookCommand request, CancellationToken cancellationToken)
		{
			var book = await _bookRepository.TryFindByIdAsync(request.Id, cancellationToken);


			if (book == null)
			{
				return;
			}

			_bookRepository.Delete(book);

			await _bookRepository.SaveChangesAsync(cancellationToken);
		}
	}
}
