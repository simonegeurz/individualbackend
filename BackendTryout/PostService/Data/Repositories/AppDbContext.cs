using Microsoft.EntityFrameworkCore;
using PostService.Models;

namespace PostService.Data.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Post>? newpostsSimone { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}