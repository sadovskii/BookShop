using BookShop.Api.EF.Repositories;
using BookShop.Api.EF.Repositories.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Api.EF.DependencyInjection
{
	public static class ServiceCollectionExtension
	{
		public static void RegisterRepositories(this IServiceCollection services)
		{
			services.AddScoped<IBookRepository, BookRepository>();
		}
	}
}
