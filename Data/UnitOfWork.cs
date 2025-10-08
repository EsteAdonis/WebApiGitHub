using WebApiGitHub.Interfaces;
using WebApiGitHub.Models;

namespace WebApiGitHub.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
	private readonly AppDbContext _context = context;
	private IRepository<Product>? _productRepo;

	public IRepository<Product> Products => _productRepo ??= new GenericRepository<Product>(_context);

	public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

	public void Dispose() => _context.Dispose();        
}
