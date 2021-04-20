using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public List<ImagePath> ImagePaths { get; set; }
    }
}
