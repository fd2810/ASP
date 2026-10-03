using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ShoppingCartApp.Models;

namespace ShoppingCartApp.Controllers
{
    public class CartController : Controller
    {
        private List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product { ProductID = 1, ProductName = "Laptop",   Price = 55000 },
                new Product { ProductID = 2, ProductName = "Mouse",    Price = 600 },
                new Product { ProductID = 3, ProductName = "Keyboard", Price = 1500 },
                new Product { ProductID = 4, ProductName = "Monitor",  Price = 12000 }
            };
        }

        public ActionResult AddToCart(int id)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            Product product = GetProducts().FirstOrDefault(p => p.ProductID == id);

            if (product != null)
            {
                CartItem existingItem = cart.FirstOrDefault(c => c.Product.ProductID == id);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = product, Quantity = 1 });
                }
            }

            Session["Cart"] = cart;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
            }
            return View(cart);
        }

        public ActionResult Remove(int id)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart != null)
            {
                CartItem item = cart.FirstOrDefault(x => x.Product.ProductID == id);
                if (item != null)
                {
                    cart.Remove(item);
                }
                Session["Cart"] = cart;
            }
            return RedirectToAction("Index");
        }
    }
}
