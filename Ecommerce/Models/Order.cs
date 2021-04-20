using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Decimal Subtotal { get; set; }

        public Decimal Taxes { get; set; }

        public Decimal Total { get; set; }

        public int Count { get; set; }
        public Customer Customer { get; set; }

        public ShippingAddress ShippingAddress { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
