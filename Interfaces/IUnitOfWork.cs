using WebApiGitHub.Models;

namespace WebApiGitHub.Interfaces;

public interface IUnitOfWork : IDisposable
{
	IRepository<Product> Products { get; }
	Task<int> SaveChangesAsync();
}
