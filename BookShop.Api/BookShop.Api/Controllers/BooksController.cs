using AutoMapper;
using BookShop.Api.EF.Entities;
using BookShop.Api.EF.Repositories.Abstract;
using BookShop.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBookRepository _bookRepository;
		private readonly IMapper _mapper;

		public BooksController(IBookRepository bookRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var books = await _bookRepository.GetAllAsync();

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
		public async Task<IActionResult> Add([FromBody] BookDto model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var book = _mapper.Map<Book>(model);

			_bookRepository.Add(book);
			await _bookRepository.SaveChangesAsync();

			return Created();
		}

		[HttpPut("id")]
		public async Task<IActionResult> Update(Guid id, [FromBody] BookDto model, CancellationToken cancellationToken)
		{
			var existingBook = await _bookRepository.TryFindByIdAsync(id);
			if (existingBook == null)
			{
				return NotFound();
			}


			_mapper.Map(model, existingBook);

			_bookRepository.Update(existingBook);
			await _bookRepository.SaveChangesAsync();

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(Guid id)
		{
			var book = await _bookRepository.TryFindByIdAsync(id);

			if (book is not null)
			{
				_bookRepository.Delete(book);
				await _bookRepository.SaveChangesAsync();
			}


			return NoContent();
		}
	}
}
