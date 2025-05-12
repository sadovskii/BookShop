using MediatR;

namespace BookShop.Api.Application.Commands
{
	public class RemoveBookCommand : IRequest
	{
		public Guid Id { get; }

		public RemoveBookCommand(Guid id)
		{
			Id = id;
		}
	}
}
