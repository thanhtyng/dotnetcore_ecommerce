using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ServiceResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public T Data { get; set; }
    }
}
