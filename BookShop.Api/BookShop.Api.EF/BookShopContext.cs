using BookShop.Api.EF.Configurations;
using BookShop.Api.EF.Entities;
using BookShop.Api.EF.Types;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Api.EF
{
	public class BookShopContext : DbContext
	{
		public DbSet<Book> Books { get; set; }

		public BookShopContext(DbContextOptions<BookShopContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BookConfiguration());

			modelBuilder.Entity<Book>().HasData(
				new Book
				{
					Id = Guid.NewGuid(),
					Author = "Jack London",
					Name = "Martin Eden",
					Pages = 500,
					BookGenre = BookGenre.Fiction
				},
				new Book
				{
					Id = Guid.NewGuid(),
					Author = "Robert C. Martin",
					Name = "Clean Code",
					Pages = 464,
					BookGenre = BookGenre.NonFiction
				}
			);
		}
	}
}
