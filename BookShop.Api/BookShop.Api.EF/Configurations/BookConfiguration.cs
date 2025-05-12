using BookShop.Api.Domain.Types;
using BookShop.Api.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookShop.Api.EF.Configurations
{
	internal class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable(nameof(Book));
			builder.HasKey(x => x.Id);
			builder.Property(x => x.BookGenre).HasConversion(new EnumToNumberConverter<BookGenre, uint>());
		}
	}
}
