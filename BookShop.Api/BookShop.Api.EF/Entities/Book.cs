using BookShop.Api.EF.Entities.Abstract;
using BookShop.Api.EF.Types;

namespace BookShop.Api.EF.Entities
{
	public class Book : DbEntity
	{
		public string Name { get; set; } = null!;

		public string Author { get; set; } = null!;

		public int Pages { get; set; }

		public BookGenre BookGenre { get; set; }
	}
}
