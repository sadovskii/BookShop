using AutoMapper;
using BookShop.Api.Domain.Entities;
using BookDb = BookShop.Api.EF.Entities.Book;

namespace BookShop.Api.EF.Profiles
{
	public class BookProfile : Profile
	{
		public BookProfile()
		{
			CreateMap<BookDb, Book>();

			CreateMap<Book, BookDb>();
		}
	}
}
