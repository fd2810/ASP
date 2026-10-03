# Practical 7 – Creating Application with Caching

**Aim:** Demonstrate data caching using `HttpContext.Cache` so that expensive data is loaded only once and then served from cache.

---

## Step 1: Create Project

1. Create new project: **ASP.NET Web Application (.NET Framework)**
2. Name: `CachingDemo`
3. Template: **MVC**

---

## Step 2: Create Product Model

### File: `Models/Product.cs`

```csharp
namespace CachingDemo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
```

---

## Step 3: Add Caching Logic in HomeController

### File: `Controllers/HomeController.cs`

```csharp
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

                // Store in cache for 5 minutes
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
```

---

## Step 4: Create View

### File: `Views/Home/Index.cshtml`

```html
@model IEnumerable<CachingDemo.Models.Product>

@{
    ViewBag.Title = "Caching Demo";
}

<h2>Caching Demo</h2>

<p><strong>@ViewBag.Message</strong></p>

<table border="1" cellpadding="10">
    <tr>
        <th>ID</th>
        <th>Product Name</th>
        <th>Price</th>
    </tr>
    @foreach (var product in Model)
    {
        <tr>
            <td>@product.Id</td>
            <td>@product.Name</td>
            <td>₹@product.Price</td>
        </tr>
    }
</table>
```

---

## Step 5: Test the Cache

1. Press **Ctrl + F5**.
2. **First request** (`/Home/Index`):
   - Message: "Data loaded from database and stored in cache."
   - Takes ~3 seconds (because of `Thread.Sleep`).
3. **Refresh the page (F5)**:
   - Message: "Data loaded from cache."
   - Loads instantly.

---

## How Caching Works

| Request | Source     | Speed   |
|---------|------------|---------|
| 1st     | Database   | Slow    |
| 2nd–nth | Cache      | Fast    |

Cache expires after **5 minutes**.
