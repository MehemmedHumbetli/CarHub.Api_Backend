using Domain.Entities;

namespace Repository.Repositories;

public interface ICartRepository
{
    #region Basic CRUD
    Task AddAsync(Cart cart);         
    void Update(Cart cart);              
    Task DeleteAsync(int cartId);        
    Task<Cart> GetByIdAsync(int id);    
    IQueryable<Cart> GetAll();           
    #endregion

    
    Task<Cart> GetUserCartAsync(string userId); // user id-sine uygun olan sebeti qaytarir


    Task<Cart> GetCartWithLinesAsync(int cartId); // sebeti ve onun cartline-larini qaytarir


    Task AddProductToCartAsync(int cartId, int productId, int quantity, decimal unitPrice); // sebetde verilen mehsulu elave edir


    Task RemoveProductFromCartAsync(int cartId, int productId); // sebetde bir dene verilen mehsulu silir


    Task ClearCartAsync(int cartId); // sebetin cartlinelerini silir 

    
    Task<decimal> GetTotalPriceAsync(int cartId);
}