using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiGitHub.Data;
using WebApiGitHub.Models;

namespace WebApiGitHub.Features.Players.GetPlayerById;

public class GetPlayerByIdQueryHandler(VideGameAppDbContext _context) : IRequestHandler<GetPlayerByIdQuery, Player?>
{
	public async Task<Player?> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
	{
		var player = await _context.Players.FindAsync(request.Id);
		return player;
	}
}

public class GetAllPlayersQueryHandler(VideGameAppDbContext _context) : IRequestHandler<GetAllPlayersQuery, List<Player>>
{
	public async Task<List<Player>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
	{
		var players = await _context.Players.ToListAsync();
		return players;
	}
}