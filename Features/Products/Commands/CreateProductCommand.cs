using MediatR;
namespace WebApiGitHub.Features.Products.Commands;

public class CreateProductCommand : IRequest<int>
{
	public required string Name { get; set; }
	public decimal Price { get; set; } = 0;
}
