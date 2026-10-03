# Practical 4 – Creating a Shopping Cart Application

**Aim:** Build a simple shopping cart using Session state in ASP.NET MVC.

---

## Step 1: Create Project

1. Open Visual Studio 2019 → **Create a new project**.
2. Select **ASP.NET Web Application (.NET Framework)**.
3. Project Name: `ShoppingCartApp`
4. Framework: .NET Framework 4.7.2
5. Select **MVC** + **No Authentication** → Create.

---

## Step 2: Create Models

### File: `Models/Product.cs`

```csharp
namespace ShoppingCartApp.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
```

### File: `Models/CartItem.cs`

```csharp
namespace ShoppingCartApp.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
```

---

## Step 3: Home Controller (Product List)

### File: `Controllers/HomeController.cs`

```csharp
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
```

---

## Step 4: Create Home View

### File: `Views/Home/Index.cshtml`

```html
@model IEnumerable<ShoppingCartApp.Models.Product>

<h2>Products</h2>

<table class="table table-bordered">
    <tr>
        <th>ID</th>
        <th>Name</th>
        <th>Price</th>
        <th>Action</th>
    </tr>
    @foreach (var item in Model)
    {
        <tr>
            <td>@item.ProductID</td>
            <td>@item.ProductName</td>
            <td>@item.Price</td>
            <td>
                @Html.ActionLink("Add To Cart", "AddToCart", "Cart", new { id = item.ProductID }, null)
            </td>
        </tr>
    }
</table>
```

---

## Step 5: Create Cart Controller

### File: `Controllers/CartController.cs`

```csharp
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ShoppingCartApp.Models;

namespace ShoppingCartApp.Controllers
{
    public class CartController : Controller
    {
        // Sample product list (same as Home)
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
```

---

## Step 6: Create Cart View

### File: `Views/Cart/Index.cshtml`

```html
@model IEnumerable<ShoppingCartApp.Models.CartItem>

<h2>Shopping Cart</h2>

@if (Model != null && Model.Any())
{
    <table class="table table-bordered">
        <tr>
            <th>Product</th>
            <th>Price</th>
            <th>Quantity</th>
            <th>Total</th>
            <th>Action</th>
        </tr>
        @{
            decimal grandTotal = 0;
        }
        @foreach (var item in Model)
        {
            decimal total = item.Product.Price * item.Quantity;
            grandTotal += total;
            <tr>
                <td>@item.Product.ProductName</td>
                <td>@item.Product.Price</td>
                <td>@item.Quantity</td>
                <td>@total</td>
                <td>
                    @Html.ActionLink("Remove", "Remove", new { id = item.Product.ProductID })
                </td>
            </tr>
        }
        <tr>
            <td colspan="3"><strong>Grand Total</strong></td>
            <td colspan="2"><strong>@grandTotal</strong></td>
        </tr>
    </table>
}
else
{
    <p>Your cart is empty.</p>
}

<p>@Html.ActionLink("Continue Shopping", "Index", "Home")</p>
```

---

## Step 7: Add Cart Link in Layout

Open `Views/Shared/_Layout.cshtml` and add inside the navbar menu:

```html
<li>@Html.ActionLink("Cart", "Index", "Cart")</li>
```

---

## Step 8: Run & Test

1. Press **F5**.
2. Click **Add To Cart** for products.
3. Click **Cart** to view items.
4. Test **Remove**.
5. Verify quantity increases when same product is added again.

**Sample Output – Cart**

| Product  | Price  | Quantity | Total  |
|----------|--------|----------|--------|
| Laptop   | 55000  | 1        | 55000  |
| Mouse    | 600    | 2        | 1200   |
| **Grand Total** |     |          | **56200** |
