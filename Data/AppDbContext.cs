using Microsoft.EntityFrameworkCore;
using WebApiGitHub.Models;

namespace WebApiGitHub.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext (options)
{
	public DbSet<Product> Products { get; set; }
}
