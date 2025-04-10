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
        //var existingCart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == cart.UserId && !c.IsDeleted);
        //if (existingCart != null)
        //{
        //    throw new BadRequestException("User already has a cart.");
        //}
        
        await _context.Carts.AddAsync(cart);
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

    public async Task ClearCartLineAsync(int cartId)
    {
        var carts = await  _context.Carts.Include(c => c.CartLines).FirstOrDefaultAsync(c => c.Id == cartId);
        _context.CartLines.RemoveRange(carts.CartLines);
        await _context.SaveChangesAsync();

    }

    public async Task DeleteAsync(int cartId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
        cart.IsDeleted = true;
        cart.DeletedDate = DateTime.Now;
        cart.DeletedBy = 0;
        await _context.SaveChangesAsync();
    }

    public async Task<Cart> GetCartWithLinesAsync(int cartId)
    {
        return await _context.Carts.Include(c => c.CartLines.Where(cl => !cl.IsDeleted)).FirstOrDefaultAsync(c => c.Id == cartId && !c.IsDeleted);

    }  

    public async Task<decimal> GetTotalPriceAsync(int cartId) 
    {
        var cart = await _context.Carts.Include(c => c.CartLines).FirstOrDefaultAsync(c => c.Id == cartId);
        return cart.CartLines.Sum(cl => cl.Quantity * cl.UnitPrice);
    }

    public async Task<Cart> GetCartWithLinesByUserId(int userId)
    {
        return await _context.Carts.Include(c => c.CartLines).ThenInclude(cl => cl.Product).FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task RemoveProductFromCartAsync(int cartId, int productId)
    {
        
        var cart = await _context.Carts.Include(c => c.CartLines).FirstOrDefaultAsync(c => c.Id == cartId);
        var cartLine = cart?.CartLines.FirstOrDefault(cl => cl.ProductId == productId);

        if (cartLine != null)
        {
            cartLine.IsDeleted = true;
            cartLine.DeletedDate = DateTime.Now;
            cartLine.DeletedBy = 0; 
            await _context.SaveChangesAsync();
        }
    }

    public void Update(Cart cart)
    {
        throw new NotImplementedException();
    }
}
