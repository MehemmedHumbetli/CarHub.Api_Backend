using Domain.Entities;

namespace Repository.Repositories;

public interface ICartRepository
{
    
    Task AddAsync(Cart cart);  // sadece cart eleva edir cartline yox userin sebeti olur 
    void Update(Cart cart);              
    Task DeleteAsync(int cartId);  // sebeti ve cartlineliri silir obsim ne var ne yox silir enke biseydi 
   
    Task<Cart> GetCartWithLinesByUserId(int userId); // user id-sine uygun olan sebeti qaytarir
    Task<Cart> GetCartWithLinesAsync(int cartId); // sebeti ve onun cartline-larini qaytarir
    Task AddProductToCartAsync(int cartId, int productId, int quantity, decimal unitPrice); // sebetde verilen mehsulu elave edir +
    Task RemoveProductFromCartAsync(int cartId, int productId); // sebetde bir dene verilen mehsulu silir
    Task ClearCartLineAsync(int cartId); // sebetin cartlinelerini silir 
    Task<decimal> GetTotalPriceAsync(int cartId); // sebetin umumi qiymetini qaytarir
}