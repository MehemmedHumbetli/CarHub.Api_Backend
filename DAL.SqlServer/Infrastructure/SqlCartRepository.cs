using Common.Exceptions;
using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlCartRepository(string connectionString , AppDbContext context) : BaseSqlRepository(connectionString), ICartRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Cart cart)
    {
        await _context.Carts.AddRangeAsync(cart);
        await _context.SaveChangesAsync();
        
    }

    public async Task AddProductToCartAsync(int cartId, int productId, int quantity, decimal unitPrice)
    {
       var cart =  await _context.Carts.Include(c => c.CartLines).FirstOrDefaultAsync(c => c.Id == cartId);
       var existingCartLine = cart.CartLines.FirstOrDefault(cl => cl.ProductId == productId);

        if (existingCartLine != null)
        {
            existingCartLine.Quantity += quantity;
        }
        else
        {
            var newCartLine = new CartLine
            {
                CartId = cartId,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice
            };
            cart.CartLines.Add(newCartLine);
        }
        await _context.SaveChangesAsync();
    }

    public async Task ClearCartAsync(int cartId)
    {
        var carts = await  _context.Carts.Include(c => c.CartLines).FirstOrDefaultAsync(c => c.Id == cartId);
        _context.CartLines.RemoveRange(carts.CartLines);
        await _context.SaveChangesAsync();

    }

    public async Task DeleteAsync(int cartId)
    {
        await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Cart> GetAll()
    {
        return _context.Carts;
    }

    public Task<Cart> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> GetCartWithLinesAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetTotalPriceAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> GetUserCartAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveProductFromCartAsync(int cartId, int productId)
    {
        throw new NotImplementedException();
    }

    public void Update(Cart cart)
    {
        throw new NotImplementedException();
    }
}
