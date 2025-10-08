using MediatR;
using WebApiGitHub.Data;
using WebApiGitHub.Features.Products.Commands;
using WebApiGitHub.Models;

namespace WebApiGitHub.Features.Products.Handlers;

public class CreateProductHandler(VideGameAppDbContext context) : IRequestHandler<CreateProductCommand, int>
{
	private readonly VideGameAppDbContext _context = context;

	public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		var product = new Product { Name = request.Name, Price = request.Price };
		_context.Products.Add(product);
		await _context.SaveChangesAsync(cancellationToken);
		return product.Id;
	}
}
