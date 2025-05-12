using BookShop.Api.EF.Entities.Abstract;
using BookShop.Api.EF.Types;

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
