# Practical 1 – Create a Simple ASP.NET Core / MVC Web Application

**Aim:** Create a basic web application, display "Hello World", and build a simple form.

---

## Example 1: Create Project

### Steps

1. Open **Visual Studio 2019**.
2. Click **Create a new project**.
3. Search for **ASP.NET Core Web App** (or **ASP.NET Web Application (.NET Framework)**).
4. Click **Next**.
5. Configure:
   - **Project Name:** `HelloWebApp`
   - **Location:** Choose any folder
6. Click **Create**.
7. Select **Web Application (Model-View-Controller)** or **Empty**.
8. Click **Create**.

Visual Studio creates the project automatically.

---

## Example 2: Display "Hello World"

### File: `Controllers/HomeController.cs`

Replace the `Index` action with the following code:

```csharp
using Microsoft.AspNetCore.Mvc;
// For .NET Framework use: using System.Web.Mvc;

namespace HelloWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()   // or ActionResult for Framework
        {
            return Content("Hello World from ASP.NET Core!");
        }
    }
}
```

### Run the Application

- Press **F5** or click **IIS Express**.
- Browser opens automatically.

**Output:**
```
Hello World from ASP.NET Core!
```

---

## Example 3: Create a Simple Form

### 1. Update `Controllers/HomeController.cs`

```csharp
using Microsoft.AspNetCore.Mvc;
// For .NET Framework: using System.Web.Mvc;

namespace HelloWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
            ViewBag.Message = "Welcome " + name;
            return View();
        }
    }
}
```

### 2. Create / Update View `Views/Home/Index.cshtml`

```html
<form method="post">
    <label>Name:</label>
    <input type="text" name="name" />
    <button type="submit">Submit</button>
</form>

@if (ViewBag.Message != null)
{
    <h2>@ViewBag.Message</h2>
}
```

### Run

1. Press **F5**.
2. Enter a name (example: `John`) and click **Submit**.

**Output:**
```
Welcome John
```

---

## Example 4: Manual Testing Checklist

- [ ] Application starts without error
- [ ] Home page loads
- [ ] Form accepts input
- [ ] Correct welcome message is displayed

---

## Project Structure (after completion)

```
HelloWebApp/
├── Controllers/
│   └── HomeController.cs
├── Views/
│   └── Home/
│       └── Index.cshtml
└── ...
```
