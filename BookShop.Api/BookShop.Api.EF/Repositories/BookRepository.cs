using AutoMapper;
using BookShop.Api.Domain.Entities;
using BookShop.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using BookDb = BookShop.Api.EF.Entities.Book;

namespace BookShop.Api.EF.Repositories
{
	public class BookRepository : Repository<Book, BookDb>, IBookRepository
	{
		public BookRepository(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
		{
		}
	}
}
