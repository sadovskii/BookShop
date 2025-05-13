using BookShop.Api.EF.Types;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Api.Models
{
	public class BookDto
	{
		[Required]
		[MaxLength(255)]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(255)]
		public string Author { get; set; } = null!;

		[Required]
		[Range(1, 5000)]
		public int Pages { get; set; }

		[Required]
		public BookGenre BookGenre { get; set; }
	}
}
