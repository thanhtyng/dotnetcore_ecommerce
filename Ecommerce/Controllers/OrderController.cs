using Ecommerce.Models;
using Ecommerce.Services.Order;
using Ecommerce.Services.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        private readonly IOrderService _orderService;
        private readonly IUtilsService _utilsService;
        public OrderController(IOrderService orderService, IUtilsService utilsService, EcommerceDbContext context)
        {
            _context = context;
            _orderService = orderService;
            _utilsService = utilsService;
        }

        // GET api/<OrderController>/5
        [Authorize(Roles = "RegisteredCustomer")]
        [HttpGet("/api/order/purchased-history")]
        public ActionResult GetPurchasedOrder()
        {
                var authorization = Request.Headers[HeaderNames.Authorization];
                var currentAccount = _utilsService.ParseJwtToAccountObject(authorization);
                var accountId = currentAccount.Id;
                var serviceResponse = _orderService.GetOrdersByAccountId(accountId);
                return Ok(serviceResponse);
        }

        // POST api/<OrderController>
        [Authorize(Roles = "RegisteredCustomer")]
        [HttpPost("/api/order/registereduser")]
        public ActionResult GenerateOrderForRegisteredUser([FromBody] Order order)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            var currentAccount = _utilsService.ParseJwtToAccountObject(authorization);
            order.Customer.AccountId = currentAccount.Id;

            var serviceResponse = _orderService.CreateOrder(order);
            return Ok(serviceResponse);
        }

        // POST api/<OrderController>
        [HttpPost("/api/order/guest")]
        public ActionResult GenerateOrderForAnonymousUser([FromBody] Order order)
        {
            var serviceResponse = _orderService.CreateOrder(order);
            return Ok(serviceResponse);
        }
    }
}
