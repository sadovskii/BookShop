using BookShop.Api.Domain.Entities.Abstract;
using BookShop.Api.Domain.Types;

namespace BookShop.Api.Domain.Entities
{
	public class Book : Entity
	{
		public string Name { get; private set; }

		public string Author { get; private set; }

		public int Pages { get; private set; }

		public BookGenre BookGenre { get; private set; }


		public Book(string name, string author, int pages, BookGenre bookGenre)
		{
			Id = Guid.NewGuid();
			Name = name;
			Author = author;
			Pages = pages;
			BookGenre = bookGenre;
		}
	}
}
