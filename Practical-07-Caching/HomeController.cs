using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Caching;
using CachingDemo.Models;

namespace CachingDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string cacheKey = "ProductList";

            List<Product> products = HttpContext.Cache[cacheKey] as List<Product>;

            if (products == null)
            {
                // Simulate slow database call
                System.Threading.Thread.Sleep(3000);

                products = new List<Product>
                {
                    new Product { Id = 1, Name = "Laptop", Price = 55000 },
                    new Product { Id = 2, Name = "Mobile", Price = 25000 },
                    new Product { Id = 3, Name = "Tablet", Price = 30000 }
                };

                HttpContext.Cache.Insert(
                    cacheKey,
                    products,
                    null,
                    DateTime.Now.AddMinutes(5),
                    Cache.NoSlidingExpiration
                );

                ViewBag.Message = "Data loaded from database and stored in cache.";
            }
            else
            {
                ViewBag.Message = "Data loaded from cache.";
            }

            return View(products);
        }
    }
}
