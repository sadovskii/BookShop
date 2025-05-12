using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		[HttpGet]
		public string GetAll()
		{
			return "GetAll";
		}

		[HttpGet]
		[Route("{id}")]
		public string Get(int id)
		{
			return $"get with id = {id}";
		}
	}
}
