using BookShop.Api.Application.Commands;
using BookShop.Api.Domain.Entities;
using BookShop.Api.Domain.Repositories;
using MediatR;

namespace BookShop.Api.Application.CommandHandlers
{
	public class AddBookCommnadHandler : IRequestHandler<AddBookCommand>
	{
		private readonly IBookRepository _bookRepository;

		public AddBookCommnadHandler(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task Handle(AddBookCommand request, CancellationToken cancellationToken)
		{
			var book = new Book(request.Name, request.Author, request.Pages, request.BookGenre);

			_bookRepository.Add(book);

			await _bookRepository.SaveChangesAsync();
		}
	}
}
