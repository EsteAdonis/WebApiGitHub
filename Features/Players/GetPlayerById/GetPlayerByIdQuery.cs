using MediatR;
using WebApiGitHub.Models;

namespace WebApiGitHub.Features.Players.GetPlayerById;

public record GetPlayerByIdQuery(int Id) : IRequest<Player> { }

public record GetAllPlayersQuery() : IRequest<List<Player>> { }