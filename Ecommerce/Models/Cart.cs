using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int AccountId { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

    }
}
