using Mango.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.OrderAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<OrderHeader> OrderHeaders { get; set; } /*= default!;*/
    public DbSet<OrderDetails> OrderDetails { get; set; } = default!;
}
