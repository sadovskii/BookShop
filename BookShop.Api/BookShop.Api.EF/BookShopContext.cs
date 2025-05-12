using BookShop.Api.EF.Configurations;
using BookShop.Api.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Api.EF
{
	internal class BookShopContext : DbContext
	{
		public DbSet<Book> Books { get; set; }

		public BookShopContext(DbContextOptions<BookShopContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BookConfiguration());
		}
	}
}
