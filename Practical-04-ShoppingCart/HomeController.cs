using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCartApp.Models;

namespace ShoppingCartApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Product> products = new List<Product>()
            {
                new Product { ProductID = 1, ProductName = "Laptop",   Price = 55000 },
                new Product { ProductID = 2, ProductName = "Mouse",    Price = 600 },
                new Product { ProductID = 3, ProductName = "Keyboard", Price = 1500 },
                new Product { ProductID = 4, ProductName = "Monitor",  Price = 12000 }
            };

            return View(products);
        }
    }
}
