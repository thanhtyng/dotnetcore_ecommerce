using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }

        public Decimal Price { get; set; }

        public Decimal ShippingCost { get; set; }

        public string ImageUrl { get; set; }
        
        public List<Comment> CommentItems { get; set; }
    }
}
