//using AutoMapper;
//using BookShop.Api.Controllers;
//using BookShop.Api.EF.Entities;
//using BookShop.Api.EF.Repositories.Abstract;
//using BookShop.Api.Models;
//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using Moq;

//namespace BookShop.Api.Tests
//{
//	[TestFixture]
//	public class BooksControllerTests
//	{
//		private Mock<IBookRepository> _bookRepositoryMock;
//		private Mock<IMapper> _mapperMock;
//		private BooksController _controller;

//		[SetUp]
//		public void Setup()
//		{
//			_bookRepositoryMock = new Mock<IBookRepository>();
//			_mapperMock = new Mock<IMapper>();
//			_controller = new BooksController(_bookRepositoryMock.Object, _mapperMock.Object);
//		}

//		[Test]
//		public async Task GetAll_ReturnsOkResult_WithListOfBooks()
//		{
//			// Arrange
//			var books = new List<Book>
//			{
//				new Book { Id = Guid.NewGuid(), Name = "Book 1" },
//				new Book { Id = Guid.NewGuid(), Name = "Book 2" }
//			};
//			_bookRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

//			// Act
//			var result = await _controller.GetAll();

//			// Assert
//			Assert.That(result, Is.InstanceOf<OkObjectResult>());
//			var okResult = (OkObjectResult)result;
//			Assert.That(okResult.Value, Is.EqualTo(books));
//		}

//		[Test]
//		public async Task Get_ReturnsNotFound_WhenBookNotFound()
//		{
//			// Arrange
//			var id = Guid.NewGuid();
//			_bookRepositoryMock.Setup(r => r.TryFindByIdAsync(id)).ReturnsAsync((Book)null);

//			// Act
//			var result = await _controller.Get(id);

//			// Assert
//			Assert.That(result, Is.InstanceOf<NotFoundResult>());
//		}

//		[Test]
//		public async Task Get_ReturnsOkResult_WithBook()
//		{
//			// Arrange
//			var id = Guid.NewGuid();
//			var book = new Book { Id = id, Name = "Test Book", Description = "Test Desc" };
//			_bookRepositoryMock.Setup(r => r.TryFindByIdAsync(id)).ReturnsAsync(book);

//			// Act
//			var result = await _controller.Get(id);

//			// Assert
//			Assert.That(result, Is.InstanceOf<OkObjectResult>());
//			var okResult = (OkObjectResult)result;
//			Assert.That(okResult.Value, Is.EqualTo(book));
//		}

//		[Test]
//		public async Task Add_ReturnsCreatedResult()
//		{
//			// Arrange
//			var bookDto = new BookDto { Name = "New Book", Description = "New Desc" };
//			var mappedBook = new Book { Id = Guid.NewGuid(), Name = bookDto.Name, Description = bookDto.Description };
//			_mapperMock.Setup(m => m.Map<Book>(bookDto)).Returns(mappedBook);

//			// Act
//			var result = await _controller.Add(bookDto, CancellationToken.None);

//			// Assert
//			Assert.IsInstanceOf<CreatedResult>(result);
//			_bookRepositoryMock.Verify(r => r.Add(mappedBook), Times.Once);
//			_bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
//		}

//		[Test]
//		public async Task Update_ReturnsNotFound_WhenBookDoesNotExist()
//		{
//			// Arrange
//			var id = Guid.NewGuid();
//			var bookDto = new BookDto { Name = "Updated Book", Description = "Updated Desc" };
//			_bookRepositoryMock.Setup(r => r.TryFindByIdAsync(id)).ReturnsAsync((Book)null);

//			// Act
//			var result = await _controller.Update(id, bookDto, CancellationToken.None);

//			// Assert
//			Assert.IsInstanceOf<NotFoundResult>(result);
//		}

//		[Test]
//		public async Task Update_ReturnsOkResult_WhenBookUpdated()
//		{
//			// Arrange
//			var id = Guid.NewGuid();
//			var bookDto = new BookDto { Name = "Updated Book", Description = "Updated Desc" };
//			var existingBook = new Book { Id = id, Name = "Old Book", Description = "Old Desc" };
//			_bookRepositoryMock.Setup(r => r.TryFindByIdAsync(id)).ReturnsAsync(existingBook);

//			// Act
//			var result = await _controller.Update(id, bookDto, CancellationToken.None);

//			// Assert
//			Assert.IsInstanceOf<OkResult>(result);
//			_mapperMock.Verify(m => m.Map(bookDto, existingBook), Times.Once);
//			_bookRepositoryMock.Verify(r => r.Update(existingBook), Times.Once);
//			_bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
//		}

//		[Test]
//		public async Task PatchBook_ReturnsBadRequest_WhenPatchDocumentIsNull()
//		{
//			// Arrange
//			var id = Guid.NewGuid();

//			// Act
//			var result = await _controller.PatchBook(id, null);

//			// Assert
//			Assert.IsInstanceOf<BadRequestObjectResult>(result);
//			var badResult = (BadRequestObjectResult)result;
//			Assert.AreEqual("Patch document cannot be null.", badResult.Value);
//		}

//		[Test]
//		public async Task PatchBook_ReturnsNotFound_WhenBookNotFound()
//		{
//			// Arrange
//			var id = Guid.NewGuid();
//			_bookRepositoryMock.Setup(r => r.TryFindByIdAsync(id)).ReturnsAsync((Book)null);
//			var patchDoc = new JsonPatchDocument<BookDto>();

//			// Act
//			var result = await _controller.PatchBook(id, patchDoc);

//			// Assert
//			Assert.IsInstanceOf<NotFoundResult>(result);
//		}

//		[Test]
//		public async Task PatchBook_ReturnsBadRequest_WhenModelStateIsInvalid()
//		{
//			// Arrange
//			var id = Guid.NewGuid();
//			var existingBook = new Book { Id = id, Name = "Book", Description = "Desc" };
//			_bookRepositoryMock.Setup(r => r.TryFindByIdAsync(id)).ReturnsAsync(existingBook);
//			var patchDoc = new JsonPatchDocument<BookDto>();

//			// Simulate a model state error
//			_controller.ModelState.AddModelError("Name", "The Name field is required.");

//			// Act
//			var result = await _controller.PatchBook(id, patchDoc);

//			// Assert
//			Assert.IsInstanceOf<BadRequestObjectResult>(result);
//			var badResult = (BadRequestObjectResult)result;
//			Assert.IsInstanceOf<SerializableError>(badResult.Value);
//		}

//		[Test]
//		public async Task PatchBook_ReturnsOkResult_WhenPatchingSuccessful()
//		{
//			// Arrange
//			var id = Guid.NewGuid();
//			var existingBook = new Book { Id = id, Name = "Old Name", Description = "Old Desc" };
//			_bookRepositoryMock.Setup(r => r.TryFindByIdAsync(id)).ReturnsAsync(existingBook);

//			// Prepare a valid patch document that replaces the Name property.
//			var patchDoc = new JsonPatchDocument<BookDto>();
//			patchDoc.Replace(b => b.Name, "New Name");

//			// Setup the mapping from entity to DTO.
//			var bookDto = new BookDto { Name = existingBook.Name, Description = existingBook.Description };
//			_mapperMock.Setup(m => m.Map<BookDto>(existingBook)).Returns(bookDto);
//			_mapperMock.Setup(m => m.Map(bookDto, existingBook)).Verifiable();

//			// Act
//			var result = await _controller.PatchBook(id, patchDoc);

//			// Assert
//			Assert.IsInstanceOf<OkResult>(result);
//			_mapperMock.Verify(m => m.Map(bookDto, existingBook), Times.Once);
//			_bookRepositoryMock.Verify(r => r.Update(existingBook), Times.Once);
//			_bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
//		}

//		[Test]
//		public async Task Remove_ReturnsNoContent_WhenBookExists()
//		{
//			// Arrange
//			var id = Guid.NewGuid();
//			var existingBook = new Book { Id = id, Name = "Book to Delete" };
//			_bookRepositoryMock.Setup(r => r.TryFindByIdAsync(id)).ReturnsAsync(existingBook);

//			// Act
//			var result = await _controller.Remove(id);

//			// Assert
//			Assert.IsInstanceOf<NoContentResult>(result);
//			_bookRepositoryMock.Verify(r => r.Delete(existingBook), Times.Once);
//			_bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
//		}

//		[Test]
//		public async Task Remove_ReturnsNoContent_WhenBookDoesNotExist()
//		{
//			// Arrange
//			var id = Guid.NewGuid();
//			_bookRepositoryMock.Setup(r => r.TryFindByIdAsync(id)).ReturnsAsync((Book)null);

//			// Act
//			var result = await _controller.Remove(id);

//			// Assert
//			Assert.IsInstanceOf<NoContentResult>(result);
//			_bookRepositoryMock.Verify(r => r.Delete(It.IsAny<Book>()), Times.Never);
//			_bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
//		}
//	}
//}
