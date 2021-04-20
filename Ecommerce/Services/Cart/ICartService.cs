using Ecommerce.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Services.Cart
{
    public interface ICartService
    {
        ServiceResponse<string> GenerateCart(Models.Cart cart);
        ServiceResponse<Models.Cart> GetCartByUserId(int id);
        ServiceResponse<string> UpdateCartItemQuantity(int quantity, int cartItemId, int cartId);
        ServiceResponse<string> RemoveCartItem(int cartItemId);
        
    }
}
