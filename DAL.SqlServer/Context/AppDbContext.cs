using Domain.Entities;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace DAL.SqlServer.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }


    public DbSet<Car> Cars { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartLine> CartLines { get; set; }

}
