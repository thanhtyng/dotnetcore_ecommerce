using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ecommerce.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly EcommerceDbContext _db;
        private readonly ILogger<ProductService> _logger;

        public ProductService(EcommerceDbContext dbContext, ILogger<ProductService> logger)
        {
            _db = dbContext;
            _logger = logger;
            ImportProductsToDatabase();
        }

        public void ImportProductsToDatabase()
        {
            if (_db.Products.Count() == 0)
            {
                try
                {
                    var now = DateTime.UtcNow;

                    var beef = new Models.Product
                    {
                        CreatedOn = now,
                        UpdatedOn = now,
                        Name = "Beef",
                        Description = "Beef is the culinary name for meat from cattle, particularly skeletal muscle",
                        Price = 12.00m,
                        ShippingCost = 3.99m,
                        ImageUrl = "https://www.themealdb.com/images/category/beef.png"
                    };

                    var chicken = new Models.Product
                    {
                        CreatedOn = now,
                        UpdatedOn = now,
                        Name = "Chicken",
                        Description = "Dessert is a course that concludes a meal",
                        Price = 15.80m,
                        ShippingCost = 3.99m,
                        ImageUrl = "https://www.themealdb.com/images/category/chicken.png",
                    };

                    var dessert = new Models.Product
                    {
                        CreatedOn = now,
                        UpdatedOn = now,
                        Name = "Dessert",
                        Description = "Dessert is a course that concludes a meal.",
                        Price = 10.99m,
                        ShippingCost = 4.99m,
                        ImageUrl = "https://www.themealdb.com/images/category/dessert.png",
                    };

                    var lamb = new Models.Product
                    {
                        CreatedOn = now,
                        UpdatedOn = now,
                        Name = "Lamb",
                        Description = "Lamb, hogget, and mutton are the meat of domestic sheep",
                        Price = 10.99m,
                        ShippingCost = 3.99m,
                        ImageUrl = "https://www.themealdb.com/images/category/lamb.png",
                    };

                    var miscellaneous = new Models.Product
                    {
                        CreatedOn = now,
                        UpdatedOn = now,
                        Name = "Miscellaneous",
                        Description = "General foods that don't fit into another category",
                        Price = 11.99m,
                        ShippingCost = 2.99m,
                        ImageUrl = "https://www.themealdb.com/images/category/miscellaneous.png",
                    };

                    _db.Products.Add(beef);
                    _db.Products.Add(chicken);
                    _db.Products.Add(lamb);
                    _db.Products.Add(miscellaneous);
                    _db.Products.Add(dessert);

                    _db.SaveChanges();

                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.StackTrace);
                }
            }

        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public List<Models.Product> GetAllProducts()
        {
            return _db.Products
                           .Include(products => products.CommentItems)
                           .ToList();
        }

        /// <summary>
        /// Get a single product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.Product GetProductById(int id)
        {
               return _db.Products
                .Include(p => p.CommentItems)
                .FirstOrDefault(item => item.Id == id);
        }
    }
}
