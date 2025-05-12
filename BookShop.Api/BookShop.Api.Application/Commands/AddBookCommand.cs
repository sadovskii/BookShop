using BookShop.Api.Domain.Types;
using MediatR;

namespace BookShop.Api.Application.Commands
{
	public class AddBookCommand : IRequest
	{
		public string Name { get; private set; }

		public string Author { get; private set; }

		public int Pages { get; private set; }

		public BookGenre BookGenre { get; private set; }


		public AddBookCommand(string name, string author, int pages, BookGenre bookGenre)
		{
			Name = name;
			Author = author;
			Pages = pages;
			BookGenre = bookGenre;
		}
	}
}
