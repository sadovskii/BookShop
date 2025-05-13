using AutoMapper;
using BookShop.Api.EF.Entities;
using BookShop.Api.Models;

namespace BookShop.Api.Profiles
{
	public class BookProfile : Profile
	{
		public BookProfile()
		{
			CreateMap<BookDto, Book>();

			CreateMap<Book, BookDto>();
		}
	}
}
