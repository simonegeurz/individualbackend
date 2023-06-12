using Microsoft.EntityFrameworkCore;
using CommentService.Models;

namespace CommentService.Data.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Comment>? newCommentsSimone { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}