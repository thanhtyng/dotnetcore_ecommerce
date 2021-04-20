using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order
{
    public interface IOrderService
    {
        ServiceResponse<string> CreateOrder(Models.Order order);
        ServiceResponse<List<Models.Order>> GetOrdersByAccountId(int AccountId);
    }
}
