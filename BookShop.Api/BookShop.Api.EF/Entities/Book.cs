using BookShop.Api.Domain.Types;
using BookShop.Api.EF.Entities.Abstract;

namespace BookShop.Api.EF.Entities
{
	public class Book : DbEntity
	{
		public string Name { get; private set; } = null!;

		public string Author { get; private set; } = null!;

		public int Pages { get; private set; }

		public BookGenre BookGenre { get; private set; }
	}
}
