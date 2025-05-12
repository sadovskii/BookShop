using BookShop.Api.EF.Entities;
using BookShop.Api.EF.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBookRepository _bookRepository;

		public BooksController(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var books = await _bookRepository.GetAllAsync();

			if (books is null || books.Count() == 0)
			{
				return NotFound();
			}

			return Ok(books);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var book = await _bookRepository.TryFindByIdAsync(id);

			if (book is null)
			{
				return NotFound();
			}

			return Ok(book);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Book bookCommnad, CancellationToken cancellationToken)
		{
			//await _mediator.Send(bookCommnad, cancellationToken);
			return Created();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(Guid id)
		{
			var book = await _bookRepository.TryFindByIdAsync(id);

			if (book is null)
			{
				return NoContent();
			}

			_bookRepository.Delete(book);

			return NoContent();
		}
	}
}
