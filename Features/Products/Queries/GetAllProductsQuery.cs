using MediatR;
using WebApiGitHub.Data;
using WebApiGitHub.Models;
namespace WebApiGitHub.Features.Products.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>> {}