using Microsoft.EntityFrameworkCore;
using WebApiGitHub.Interfaces;

namespace WebApiGitHub.Data;

public class GenericRepository<T>(AppDbContext context) : IRepository<T> where T : class
{
	private readonly AppDbContext _context = context;
	private readonly DbSet<T> _dbSet = context.Set<T>();

	public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
	public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
	public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
	public void Update(T entity) => _dbSet.Update(entity);
	public void Remove(T entity) => _dbSet.Remove(entity);
}