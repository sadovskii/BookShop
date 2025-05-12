using BookShop.Api.Application.Commands;
using BookShop.Api.Queries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IMediator _mediator;

		public BooksController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var books = await _mediator.Send(new GetBooksQuery());

			if (books is null || books.Count() == 0)
			{
				return NotFound();
			}

			return Ok(books);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var book = await _mediator.Send(new GetBookQuery(id));

			if (book is null)
			{
				return NotFound();
			}

			return Ok(book);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddBookCommand bookCommnad, CancellationToken cancellationToken)
		{
			await _mediator.Send(bookCommnad, cancellationToken);
			return Created();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(Guid id)
		{
			await _mediator.Send(new RemoveBookCommand(id));
			return NoContent();
		}
	}
}
