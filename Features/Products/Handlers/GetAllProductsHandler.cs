using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiGitHub.Data;
using WebApiGitHub.Features.Products.Queries;
using WebApiGitHub.Models;

namespace WebApiGitHub.Features.Products.Handlers;

public class GetAllProductsHandler(VideGameAppDbContext context):
						 IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
	private readonly VideGameAppDbContext _context = context;

	public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
	{
		return await _context.Products.ToListAsync();
	}
}
