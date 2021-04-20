using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly EcommerceDbContext _db;
        private readonly ILogger<OrderService> _logger;
        public OrderService(EcommerceDbContext dbContext, ILogger<OrderService> logger)
        {
            _logger = logger;
            _db = dbContext;
        }

        public ServiceResponse<string> CreateOrder(Models.Order order)
        {
            var now = DateTime.UtcNow;
            try
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                return new ServiceResponse<string>
                {
                    Code = 200,
                    Time = now,
                    Message = "Order was generated.",
                    Data = null
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<string>
                {
                    Code = 400,
                    Time = now,
                    Message = "Something wrong when generating an order.",
                    Data = null
                };
            }
        }

        public ServiceResponse<List<Models.Order>> GetOrdersByAccountId(int AccountId)
        {
            var now = DateTime.UtcNow;
            try
            {
                var orders = _db.Orders
                  .Include(o => o.ShippingAddress)
                  .Include(o => o.OrderItems)
                       .ThenInclude(oi => oi.Product)
                  .Include(oi => oi.Customer)
                  .Where(c => c.Customer.AccountId == AccountId).ToList();
                return new ServiceResponse<List<Models.Order>>
                {
                    Code = 200,
                    Time = now,
                    Message = "It is success",
                    Data = orders
                };

            } catch(Exception e)
            {
                return new ServiceResponse<List<Models.Order>>
                {
                    Code = 400,
                    Time = now,
                    Message = "Something wrong",
                    Data = null
                };
            }
   
        }
    }
}
