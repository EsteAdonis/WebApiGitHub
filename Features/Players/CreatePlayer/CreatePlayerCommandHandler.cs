using MediatR;
using WebApiGitHub.Data;
using WebApiGitHub.Models;

namespace WebApiGitHub.Features.Players.CreatePlayer;

public class CreatePlayerCommandHandler(VideGameAppDbContext _context) : IRequestHandler<CreatePlayerCommand, int>
{
	public async Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
	{
		var player = new Player { Name = request.Name, Level = request.Level };
		_context.Players.Add(player);
		await _context.SaveChangesAsync(cancellationToken);
		return player.Id;
	}
}