using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Product
{
    public interface IProductService
    {
        void ImportProductsToDatabase();
        List<Models.Product> GetAllProducts();
        Models.Product GetProductById(int id);
    }
}