using Ecommerce.Models;
using Ecommerce.Services.Cart;
using Ecommerce.Services.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        private readonly ICartService _cartService;
        private readonly IUtilsService _utilsService;
        public CartController(ICartService cartService, EcommerceDbContext context, IUtilsService utilsService)
        {
            _cartService = cartService;
            _context = context;
            _utilsService = utilsService;
        }

        // GET api/<CartController>/5
        [Authorize(Roles = "RegisteredCustomer")]
        [HttpGet("/api/cart")]
        public ActionResult GetCartByUser()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            var currentAccount = _utilsService.ParseJwtToAccountObject(authorization);
            var accountId = currentAccount.Id;
            var serviceResponse = _cartService.GetCartByUserId(accountId);
            return Ok(serviceResponse);
        }

        // POST api/<CartController>
        [Authorize(Roles = "RegisteredCustomer")]
        [HttpPost("/api/cart/create")]
        public ActionResult GenerateCart([FromBody] Cart cart)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            var currentAccount = _utilsService.ParseJwtToAccountObject(authorization);
            cart.AccountId = currentAccount.Id;
            var serviceResponse = _cartService.GenerateCart(cart);
            return Ok(serviceResponse);
        }


        // PUT api/<CartController>/5
        [Authorize(Roles = "RegisteredCustomer")]
        [HttpPut("{id}")]
        public ActionResult UpdateCartItemQuantity(int id, [FromBody] int cartItemId, int quantity)
        {
            var serviceResponse = _cartService.UpdateCartItemQuantity(quantity, cartItemId, id);
            return Ok(serviceResponse);
        }

        // DELETE api/<CommentsController>/5
        [Authorize(Roles = "RegisteredCustomer")]
        [HttpDelete("{id}")]
        public ActionResult DeleteCartItem(int id)
        {
            var serviceResponse = _cartService.RemoveCartItem(id);
            return Ok(serviceResponse);
        }

    }
}
