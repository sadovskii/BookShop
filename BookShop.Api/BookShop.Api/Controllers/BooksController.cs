using AutoMapper;
using BookShop.Api.EF.Entities;
using BookShop.Api.EF.Repositories.Abstract;
using BookShop.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBookRepository _bookRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<BooksController> _logger;

		public BooksController(IBookRepository bookRepository, IMapper mapper, ILogger<BooksController> logger)
		{
			_bookRepository = bookRepository;
			_mapper = mapper;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			_logger.LogInformation("Starting GetAll to retrieve all books.");
			var books = await _bookRepository.GetAllAsync();
			_logger.LogInformation("{Count} books retrieved.", books.Count());

			return Ok(books);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			_logger.LogInformation("Getting book with id: {BookId}", id);
			var book = await _bookRepository.TryFindByIdAsync(id);

			if (book is null)
			{
				_logger.LogWarning("Book with id: {BookId} was not found.", id);
				return NotFound();
			}

			_logger.LogInformation("Book with id: {BookId} successfully retrieved.", id);
			return Ok(book);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] BookDto model, CancellationToken cancellationToken)
		{
			if (model == null)
			{
				_logger.LogWarning("Received null book model in Add.");
				return BadRequest("Invalid book data.");
			}

			_logger.LogInformation("Adding a new book.");
			var book = _mapper.Map<Book>(model);

			_bookRepository.Add(book);
			await _bookRepository.SaveChangesAsync();

			_logger.LogInformation("Book added with id: {BookId}", book.Id);

			return Created();
		}

		[HttpPut("id")]
		public async Task<IActionResult> Update(Guid id, [FromBody] BookDto model, CancellationToken cancellationToken)
		{
			if (model == null)
			{
				_logger.LogWarning("Received null book model in Add.");
				return BadRequest("Invalid book data.");
			}

			_logger.LogInformation("Updating book with id: {BookId}", id);
			var existingBook = await _bookRepository.TryFindByIdAsync(id);
			if (existingBook == null)
			{
				_logger.LogWarning("Update failed. Book with id: {BookId} not found.", id);
				return NotFound();
			}

			_mapper.Map(model, existingBook);

			_bookRepository.Update(existingBook);
			await _bookRepository.SaveChangesAsync();

			_logger.LogInformation("Book with id: {BookId} updated successfully.", id);

			return Ok();
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> PatchBook(Guid id, [FromBody] JsonPatchDocument<BookDto> patchDoc)
		{
			_logger.LogInformation("Patching book with id: {BookId}", id);

			if (patchDoc == null)
			{
				_logger.LogWarning("Patch document was null for book with id: {BookId}", id);
				return BadRequest("Patch document cannot be null.");
			}

			var existingBook = await _bookRepository.TryFindByIdAsync(id);
			if (existingBook == null)
			{
				_logger.LogWarning("Patch failed. Book with id: {BookId} not found.", id);
				return NotFound();
			}

			var bookDto = _mapper.Map<BookDto>(existingBook);

			try
			{
				patchDoc.ApplyTo(bookDto, ModelState);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(ex, "Error applying patch to book with id: {BookId}", id);
				return BadRequest(ex.Message);
			}


			if (!ModelState.IsValid)
			{
				_logger.LogWarning("Model state is invalid after patching book with id: {BookId}", id);
				return BadRequest(ModelState);
			}

			_mapper.Map(bookDto, existingBook);

			_bookRepository.Update(existingBook);
			await _bookRepository.SaveChangesAsync();
			_logger.LogInformation("Book with id: {BookId} patched successfully.", id);

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(Guid id)
		{
			_logger.LogInformation("Attempting to remove book with id: {BookId}", id);
			var book = await _bookRepository.TryFindByIdAsync(id);

			if (book is not null)
			{
				_bookRepository.Delete(book);
				await _bookRepository.SaveChangesAsync();
				_logger.LogInformation("Book with id: {BookId} deleted successfully.", id);
			}
			else
			{
				_logger.LogWarning("Book with id: {BookId} not found. No deletion performed.", id);
			}


			return NoContent();
		}
	}
}
