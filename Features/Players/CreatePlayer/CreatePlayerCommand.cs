using MediatR;

namespace WebApiGitHub.Features.Players.CreatePlayer;

public record CreatePlayerCommand(string Name, int Level) : IRequest<int> {}