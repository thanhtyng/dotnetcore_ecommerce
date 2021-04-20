using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly EcommerceDbContext _db;
        private readonly ILogger<CartService> _logger;
        public CartService(EcommerceDbContext dbContext, ILogger<CartService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

      ServiceResponse<string> ICartService.GenerateCart(Models.Cart cart)
        {
            var now = DateTime.UtcNow;
            try
            {
                _db.Carts.Add(cart);
                _db.SaveChanges();
                return new ServiceResponse<string>
                {
                    Code = 200,
                    Time = now,
                    Message = "Cart was generated.",
                    Data = null
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<string>
                {
                    Code = 400,
                    Time = now,
                    Message = "Something wrong when generating a cart.",
                    Data = null
                };
            }
        }

        ServiceResponse<Models.Cart> ICartService.GetCartByUserId(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var cart = _db.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(item => item.AccountId == id);
                return new ServiceResponse<Models.Cart>
                {
                    Code = 200,
                    Time = now,
                    Message = "It is success",
                    Data = cart
                };

            }
            catch (Exception e)
            {
                return new ServiceResponse<Models.Cart>
                {
                    Code = 400,
                    Time = now,
                    Message = "Something wrong",
                    Data = null
                };
            }
        }

        ServiceResponse<string> ICartService.UpdateCartItemQuantity(int quantity, int cartItemId, int cartId)
        {

            var now = DateTime.UtcNow;
            try
            {
                var cart = _db.Carts
                          .Include(cart => cart.CartItems)
                              .ThenInclude(ci => _db.Products.Include(p => p.Id == ci.ProductId))
                          .FirstOrDefault(c => c.Id == cartId);

                if (cart.CartItems != null && cart.CartItems.Count() > 0)
                {
                    var i = _db.CartItems.FirstOrDefault(item => item.Id == cartItemId);

                    if (i != null)
                    {
                        i.Quantity = quantity;
                        _db.CartItems.Update(i);
                        _db.SaveChanges();
                    }
                }
                return new ServiceResponse<string>
                {
                    Code = 200,
                    Time = now,
                    Message = "Cart was updated.",
                    Data = null
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<string>
                {
                    Code = 400,
                    Time = now,
                    Message = "Something wrong when updating a cart.",
                    Data = null
                };
            }
        }

        ServiceResponse<string> ICartService.RemoveCartItem(int cartItemId)
        {
            var now = DateTime.UtcNow;
            try
            {
                var i = _db.CartItems.FirstOrDefault(item => item.Id == cartItemId);
                _db.CartItems.Remove(i);
                _db.SaveChanges();

                return new ServiceResponse<string>
                {
                    Code = 200,
                    Time = now,
                    Message = "Cart was deleted.",
                    Data = null
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<string>
                {
                    Code = 400,
                    Time = now,
                    Message = "Something wrong when deleting a cart.",
                    Data = null
                };
            }
         }
    }
}

