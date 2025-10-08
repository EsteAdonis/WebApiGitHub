using Microsoft.EntityFrameworkCore;
using WebApiGitHub.Models;

namespace WebApiGitHub.Data;

public class VideGameAppDbContext(DbContextOptions<VideGameAppDbContext> option) : DbContext(option)
{
	public DbSet<Player> Players { get; set; }
	public DbSet<Product> Products { get; set; }
}